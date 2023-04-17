using System.Diagnostics;
using CloudPanelApi.App.Exceptions;
using Logging.Net;
using Renci.SshNet;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace CloudPanelApi.App.Services;

public class CliService
{
    private bool UseSsh = false;

    // SSH
    private ConnectionInfo ConnectionInfo;

    public CliService()
    {
        // SSH
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SSH_HOST")))
        {
            Logger.Info("Creating ssh connection data");

            UseSsh = true;

            var host = Environment.GetEnvironmentVariable("SSH_HOST");
            var port = Environment.GetEnvironmentVariable("SSH_PORT");
            var username = Environment.GetEnvironmentVariable("SSH_USERNAME");
            var password = Environment.GetEnvironmentVariable("SSH_PASSWORD");

            if (string.IsNullOrEmpty(port) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Logger.Warn("Please specify all environment variables necessary for ssh connections");
                Environment.Exit(1);
            }

            if (!int.TryParse(port, out int portInt))
            {
                Logger.Warn("Port cannot be parsed");
                Environment.Exit(1);
            }

            ConnectionInfo = new(host, portInt, username, new PasswordAuthenticationMethod(username, password));
        }
    }

    public async Task<string> Execute(string command, Action<Dictionary<string, string>>? args = null)
    {
        Dictionary<string, string> arguments = new();
        args?.Invoke(arguments);

        var cmd = BuildCommandText(command, arguments);

        if (UseSsh) //TODO: Add ssh session storage cache thingy
            return await ExecuteSsh(cmd);

        return await ExecuteBash(cmd);
    }

    private Task<string> ExecuteSsh(string command)
    {
        using (var client = new SshClient(ConnectionInfo))
        {
            client.HostKeyReceived += (sender, args) => { args.CanTrust = true; };

            client.Connect();

            if (!client.IsConnected)
            {
                throw new Exception("Unable to connect to ssh host");
            }

            var cmd = client.CreateCommand(command);

            if (cmd == null)
                throw new Exception("Unable to create ssh command");

            cmd.Execute();

            client.Disconnect();

            //Logger.Debug("--------------------------------------------------");
            //Logger.Debug($"Result: {cmd.Result}");
            //Logger.Debug($"Error: {cmd.Error}");
            //Logger.Debug($"Exit code: {cmd.ExitStatus}");
            //Logger.Debug("--------------------------------------------------");

            if (cmd.ExitStatus != 0)
            {
                throw new CloudPanelException(
                    cmd.Result
                        .TrimStart('"')
                        .Replace("\"\n", "")
                );
            }

            return Task.FromResult(cmd.Result);
        }
    }

    private async Task<string> ExecuteBash(string command)
    {
        Process process = new Process();
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"{command}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();

        string output = await process.StandardOutput.ReadToEndAsync();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new CloudPanelException(
                output
                    .TrimStart('"')
                    .Replace("\"\n", "")
            );
        }

        return output;
    }

    private string BuildCommandText(string command, Dictionary<string, string> args)
    {
        var result = "clpctl ";

        result += command;

        foreach (var arg in args)
        {
            if (string.IsNullOrEmpty(arg.Value))
                result += $" --{arg.Key}";
            else
                result += $" --{arg.Key}='{arg.Value}'";
        }

        return result;
    }
}