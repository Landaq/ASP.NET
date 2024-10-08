﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmXmlDataSource.aspx.cs" Inherits="DevDataControl.FrmXmlDataSource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Xml Data Source 컨트롤</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DataList ID="ctlMyInterest" runat="server"
                DataSourceID="xmlMyIneterest">
                <ItemTemplate>
                    <em><%# XPath("title") %></em> : <%# XPath("description") %>
                </ItemTemplate>
            </asp:DataList>
            <asp:XmlDataSource ID="xmlMyIneterest" runat="server"
                DataFile="~/FrmXmlDataSource.xml"></asp:XmlDataSource>
        </div>
    </form>
</body>
</html>
