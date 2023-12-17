<%@ Page Title="" Language="C#" MasterPageFile="~/Prototype/TestMaster.Master" AutoEventWireup="true" CodeBehind="WebForm23.aspx.cs" Inherits="Gabay_Final_V2.Prototype.WebForm23" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Resources/CustomJS/JavaScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control TextBox1" onkeyup="validateForm()"></asp:TextBox>
    <div class="textError text-danger d-none" id="textError">
        <span><i class="bi bi-info-circle"></i></span>
        <span>No you're wrong</span>
    </div>
    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control TextBox2" onkeyup="validateForm()"></asp:TextBox>
    <div class="textError1 text-danger d-none" id="textError1">
        <span><i class="bi bi-info-circle"></i></span>
        <span>No you're wrong</span>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="d-flex" />

    <asp:Button ID="Button2" runat="server" Text="Search" OnClick="Button2_Click" UseSubmitBehavior="false" />
    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="d-flex"></asp:Label>

    <%--<script>
        const textBox = document.getElementById("<%= TextBox1.ClientID %>");
        const errorText = document.getElementById("textError");

        const textBox1 = document.getElementById("<%= TextBox2.ClientID %>");
        const errorText1 = document.getElementById("textError1");

        function checkTextbox() {
            if (textBox.value.trim() === "") {
                textBox.classList.add('is-invalid');
                textBox.classList.remove('is-valid');
                errorText.classList.remove('d-none');
                return false;
            } else {
                textBox.classList.remove('is-invalid');
                textBox.classList.add('is-valid');
                errorText.classList.add('d-none');
                return true;
            }
        }

        function checkTextbox1() {
            if (textBox1.value.trim() === "") {
                textBox1.classList.add('is-invalid');
                textBox1.classList.remove('is-valid');
                errorText1.classList.remove('d-none');
                return false;
            } else {
                textBox1.classList.remove('is-invalid');
                textBox1.classList.add('is-valid');
                errorText1.classList.add('d-none');
                return true;
            }
        }

        function validateButton1(e) {

            if (!checkTextbox()) {
                e.preventDefault();
            }

            if (!checkTextbox1()) {
                e.preventDefault();
            }
        }

        document.getElementById("Button1").addEventListener('click', validateButton1);
        textBox.addEventListener('keyup', checkTextbox);
        textBox1.addEventListener('keyup', checkTextbox1);
    </script>--%>

    <%--<script>
        document.addEventListener("DOMContentLoaded", () => {
            const form = document.querySelector('.form1');

            const textBox = form.getElementById("<%= TextBox1.ClientID %>");
            const errorText = form.getElementById("textError");

            const textBox1 = form.getElementById("<%= TextBox2.ClientID %>");
            const errorText1 = form.getElementById("textError1");

            function checkTextbox() {
                if (textBox.value.trim() === "") {
                    textBox.classList.add('is-invalid');
                    textBox.classList.remove('is-valid');
                    errorText.classList.remove('d-none');
                    return false;
                } else {
                    textBox.classList.remove('is-invalid');
                    textBox.classList.add('is-valid');
                    errorText.classList.add('d-none');
                    return true;
                }
            }

            function checkTextbox1() {
                if (textBox1.value.trim() === "") {
                    textBox1.classList.add('is-invalid');
                    textBox1.classList.remove('is-valid');
                    errorText1.classList.remove('d-none');
                    return false;
                } else {
                    textBox1.classList.remove('is-invalid');
                    textBox1.classList.add('is-valid');
                    errorText1.classList.add('d-none');
                    return true;
                }
            }

            
            textBox.addEventListener('keyup', checkTextbox);
            textBox1.addEventListener('keyup', checkTextbox1);
        });
    </script>--%>

    <%-- <script>
        document.addEventListener("DOMContentLoaded", function () {
            const form = document.getElementById("#form1");

            const textBox = form.getElementById("<%= TextBox1.ClientID %>");
            const errorText = form.getElementById("textError");

            const textBox1 = form.getElementById("<%= TextBox2.ClientID %>");
            const errorText1 = form.getElementById("textError1");

            const button1 = form.getElementById("<%= Button1.ClientID %>");

            function checkTextbox() {
                if (textBox.value.trim() === "") {
                    textBox.classList.add('is-invalid');
                    textBox.classList.remove('is-valid');
                    errorText.classList.remove('d-none');
                    return false;
                } else {
                    textBox.classList.remove('is-invalid');
                    textBox.classList.add('is-valid');
                    errorText.classList.add('d-none');
                    return true;
                }
            }

            function checkTextbox1() {
                if (textBox1.value.trim() === "") {
                    textBox1.classList.add('is-invalid');
                    textBox1.classList.remove('is-valid');
                    errorText1.classList.remove('d-none');
                    return false;
                } else {
                    textBox1.classList.remove('is-invalid');
                    textBox1.classList.add('is-valid');
                    errorText1.classList.add('d-none');
                    return true;
                }
            }

            function validateButton1() {
                const isTextBoxValid = checkTextbox();
                const isTextBox1Valid = checkTextbox1();

                // Disable Button1 if either TextBox1 or TextBox2 is not completed
                button1.disabled = !(isTextBoxValid && isTextBox1Valid);

                return isTextBoxValid && isTextBox1Valid;
            }

            textBox.addEventListener('keyup', validateButton1);
            textBox1.addEventListener('keyup', validateButton1);
        });
    </script>--%>
</asp:Content>
