#! /bin/bash

echo "Installing cloud panel api server"

mkdir /lib/cpapiserver/

curl -L -o /lib/cpapiserver/CloudPanelApi "https://github.com/Endelon-Hosting/CloudPanelApi/releases/latest/download/CloudPanelApi_linux_([[ "$(uname -m)" == "x86_64" ]] && echo "arm64")"
curl -L -o /lib/cpapiserver/CloudPanelApi.pdb "https://github.com/Endelon-Hosting/CloudPanelApi/releases/latest/download/cpapiserver_linux_([[ "$(uname -m)" == "x86_64" ]] && echo "arm64")"
curl -L -o /lib/cpapiserver/appsettings.json https://raw.githubusercontent.com/Endelon-Hosting/CloudPanelApi/main/appsettings.json

curl -L -o /etc/systemd/system/cpapiserver.service https://raw.githubusercontent.com/Endelon-Hosting/CloudPanelApi/main/cpapiserver.service
chmod 664 /etc/systemd/system/cpapiserver.service
chmod +x /lib/cpapiserver/CloudPanelApi
systemctl daemon-reload