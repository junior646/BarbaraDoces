<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Doces-e-Delicias.aspx.cs" Inherits="Doces.Doces_e_Delicias" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <style type="text/css">
        .padding-nowrap {
            white-space: nowrap;
            padding: 6px;
        }

        .nowrap {
            white-space: nowrap;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Veja a nossa lista de sabores e valores!</h1>
    <br />
    Confira logo abaixo quanto custa os nosso pães de mel e nossas trufas e seus respectivos sabores.
    <br />
    Adoce a sua festa com os nossos doces de excelentíssima qualidade e sabor.
    <br />
    <br />
    <%--Parâmetros de Pesquisa--%>
    <div id="divBody" runat="server" visible="true">
        <asp:Panel ID="pnlTabela" GroupingText="Escolha um tipo de Produto:" runat="server" Visible="true">
            <table>
                <tr>
                    <th>
                        <asp:Label ID="Label2" runat="server" Text="Tipo de Doce: " /></th>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="false" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <th>
                        <br />
                        <asp:Label ID="lblTipoEncomenda" runat="server" Text="Tipo de Compra: " /></th>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rblTipoEncomenda" runat="server" AutoPostBack="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Button ID="btnPesquisar" Text="Pesquisar" runat="server" OnClick="btnPesquisar_Click" UseSubmitBehavior="true" />
    </div>
    <%--Resultados--%>
    <div id="divResultado" runat="server" visible="false">
        <asp:Button ID="btnVoltar" Text="Voltar" runat="server" OnClick="btnVoltar_Click" UseSubmitBehavior="false" />
        <br />
        <br />
        <asp:Button ID="btnGerarRelatorio" Text="Salvar em PDF" runat="server" OnClick="btnGerarRelatorio_Click" UseSubmitBehavior="true" />
        <br />
        <asp:Panel ID="pnlGrid" GroupingText="Valores e Sabores:" runat="server" Visible="false">
            <asp:GridView ID="dgvResultado" runat="server" AutoGenerateColumns="true" AllowPaging="false">
                <RowStyle HorizontalAlign="Center" CssClass="padding-nowrap" />
                <EmptyDataTemplate>
                    Nenhum resultado encontrado!
                </EmptyDataTemplate>
                <EmptyDataRowStyle CssClass="empty-data" HorizontalAlign="Center" ForeColor="Red"
                    BackColor="White" />
            </asp:GridView>
        </asp:Panel>
        <br />
    </div>
    <%--Relatorio--%>
    <div id="divRelatorio" runat="server" visible="false">
        <div>
            <CR:CrystalReportViewer ID="crVisualizar" runat="server" AutoDataBind="True" />
        </div>
    </div>

    <br />
    <br />
    <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" Font-Bold="true" />
</asp:Content>
