#!/bin/bash

echo "Installing cloud panel api server"

mkdir /lib/cpapiserver/

curl -L -o /lib/cpapiserver/CloudPanelApi "https://github.com/Endelon-Hosting/CloudPanelApi/releases/latest/download/CloudPanelApi_linux_$(uname -m)"
curl -L -o /lib/cpapiserver/CloudPanelApi.pdb "https://github.com/Endelon-Hosting/CloudPanelApi/releases/latest/download/cpapiserver_linux_$(uname -m)"
curl -L -o /lib/cpapiserver/appsettings.json https://raw.githubusercontent.com/Endelon-Hosting/CloudPanelApi/main/appsettings.json

curl -L -o /etc/systemd/system/cpapiserver.service https://raw.githubusercontent.com/Endelon-Hosting/CloudPanelApi/main/cpapiserver.service
chmod 664 /etc/systemd/system/cpapiserver.service
chmod +x /lib/cpapiserver/CloudPanelApi
systemctl daemon-reload
systemctl enable --now cpapiserver
service cpapiserver start
API_KEY=`openssl rand -hex 28`
echo Your new random API Key: $API_KEY
echo Be sure to open port 9999 on your CloudPanel firewall too
echo Ok, cpapiserver service should be installed and running!
