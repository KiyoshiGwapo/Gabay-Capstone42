<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Student_CampusInfo.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.CampusInfo.Student_CampusInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- <script src="../../../Resources/CustomJS/CampusInfo/CampusInfo.js"></script> -->
    <link href="../../../Resources/CustomStyleSheet/CampusInfo/CampusInfo.css" rel="stylesheet" />
     <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
<!-- Add this link for Font Awesome CSS -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <div class="container">
              <div class="accordion">
                        <div class="accordion-image-container">
                            <img src="../../../Resources/Images/model.png" alt="Accordion Image" class="accordion-image" />
                        </div>
                <asp:Repeater ID="rptAccordion" runat="server">
                <ItemTemplate>
                        <div class="accordion-content" data-item-id='<%# Eval("id") %>' data-item-title='<%# Eval("Title") %>'>
                            <header>
                                <span class="title"><%# Eval("Title") %></span>
                                <i class="fa-solid fa-plus"></i>
                            </header>
                            <p class="description">
                                <br /><br /><%# Eval("Content") %><br /><br />
                            <button type="button" class="edit-button" data-toggle="modal" data-target="#editModal"
                                onclick="editAccordionItem('<%# Eval("id") %>','<%# Eval("Title") %>','<%# Eval("Content") %>')">
                                <i class="fas fa-pencil-alt"></i>
                            </button>
                            </p>
                        </div>
             </ItemTemplate>
            </asp:Repeater>
          </div>
        </div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
     <asp:Literal ID="accordionContainer" runat="server"></asp:Literal>
<!-- Modal -->
<div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="editModalLabel">Edit Content</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <!-- Add the form elements for editing content here -->
                <label for="editTitle">Title:</label>
                <asp:TextBox ID="txtNewTitle" runat="server" CssClass="form-control"></asp:TextBox>
                <br />
              <label for="editContent">Content:</label>
               <asp:TextBox ID="txtNewContent" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="reloadPage()">Close</button>
                <asp:Button ID="btnSaveChanges" runat="server" Text="Save changes" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" />       
                <asp:Button ID="btnDeleteContent" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDeleteContent_Click" />
                <asp:Button ID="btnAddNewRecord" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="btnAddNewRecord_Click" />
            </div>
        </div>
    </div>
</div>
 <asp:HiddenField ID="hdnAccordionContent" runat="server" />
 <asp:HiddenField ID="hdnAccordionIndex" runat="server" />
     <script>
        const accordionContent = document.querySelectorAll(".accordion-content");

        accordionContent.forEach((item, index) => {
            let header = item.querySelector("header");
            header.addEventListener("click", () => {
                item.classList.toggle("open");

                let description = item.querySelector(".description");
                if (item.classList.contains("open")) {
                    description.style.height = `${description.scrollHeight}px`; //scrollHeight property returns the height of an element including padding , but excluding borders, scrollbar or margin
                    item.querySelector("i").classList.replace("fa-plus", "fa-minus");
                } else {
                    description.style.height = "0px";
                    item.querySelector("i").classList.replace("fa-minus", "fa-plus");
                }
                removeOpen(index); //calling the funtion and also passing the index number of the clicked header
            })
        })

        function removeOpen(index1) {
            accordionContent.forEach((item2, index2) => {
                if (index1 != index2) {
                    item2.classList.remove("open");

                    let des = item2.querySelector(".description");
                    des.style.height = "0px";
                    item2.querySelector("i").classList.replace("fa-minus", "fa-plus");
                }
            })
        }
        function saveChanges() {
            var editedContent = document.getElementById('<%= txtNewContent.ClientID %>').value;
            var accordionIndex = document.getElementById('<%= hdnAccordionIndex.ClientID %>').value;

            // You can use AJAX to send the edited content and accordion index to the server and update the database.
            // Example AJAX code using jQuery:
            $.ajax({
                type: "POST",
                url: "Admin.aspx/SaveChanges", // Replace "Admin.aspx" with the actual path to your code-behind file
                data: JSON.stringify({ index: accordionIndex, content: editedContent }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Handle the response from the server, e.g., show success message
                    alert("Content updated successfully!");
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error, e.g., show error message
                    alert("Error updating content: " + error);
                }
            });
        }


        // Function to store the accordion content in the hidden field
        function storeAccordionContent() {
            const accordionContent = document.querySelectorAll(".accordion-content");
            const accordionData = [];

            accordionContent.forEach((item, index) => {
                let title = item.querySelector(".title").innerText.trim();
                let description = item.querySelector(".description").innerText.trim();

                accordionData.push({ title: title, description: description });
            });

            document.getElementById('<%= hdnAccordionContent.ClientID %>').value = JSON.stringify(accordionData);
        }

        // Call the function to store the accordion content when the page loads
        window.onload = storeAccordionContent;
        function editAccordionItem(itemId) {
            // Assuming you have a hidden field with ID 'hdnAccordionIndex' to store the item ID for the update
            document.getElementById('hdnAccordionIndex').value = itemId;

            // Fetch the content from the accordion item and set it in the modal inputs
            var accordionItem = document.querySelector(`[data-item-id='${itemId}']`);
            var title = accordionItem.querySelector('.title').innerText.trim();
            var description = accordionItem.querySelector('.description').innerText.trim();

            document.getElementById('<%= txtNewTitle.ClientID %>').value = title;
            document.getElementById('<%= txtNewContent.ClientID %>').value = description;
        }

        // Event delegation for accordion items
        document.querySelector('.accordion').addEventListener('click', function (event) {
            var accordionItem = event.target.closest('.accordion-content');
            if (accordionItem) {
                accordionItem.classList.toggle('open');

                var description = accordionItem.querySelector('.description');
                if (accordionItem.classList.contains('open')) {
                    description.style.height = `${description.scrollHeight}px`;
                    accordionItem.querySelector('i').classList.replace('fa-plus', 'fa-minus');
                } else {
                    description.style.height = '0px';
                    accordionItem.querySelector('i').classList.replace('fa-minus', 'fa-plus');
                }

                removeOpen(accordionItem);
            }
        });

        function removeOpen(clickedItem) {
            var accordionItems = document.querySelectorAll('.accordion-content');
            accordionItems.forEach(function (item) {
                if (item !== clickedItem && item.classList.contains('open')) {
                    item.classList.remove('open');
                    item.querySelector('.description').style.height = '0px';
                    item.querySelector('i').classList.replace('fa-minus', 'fa-plus');
                }
            });
        }
    </script>
 <script>
     // Your JavaScript code for the accordion and modal goes here
     // ...
     // Use JSON.parse to convert the JSON data from the accordionContainer to a JavaScript array of objects
     var accordionData = JSON.parse('<%= accordionContainer.Text %>');

     // Function to create an accordion item based on the data
     function createAccordionItem(data) {
         return `
        <div class='accordion-content'>
            <header>
                <span class='title'>${data.Title}</span>
                <i class='fa-solid fa-plus'></i>
            </header>
            <p class='description'>
                <br /><br />${data.Description}<br /><br />
                <button type='button' class='edit-button' data-toggle='modal' data-target='#editModal' onclick='editAccordionItem(${data.id})'>Edit</button>
            </p>
        </div>`;
     }

     // Function to add all accordion items to the accordion container
     function populateAccordion() {
         var accordionContainer = document.querySelector('.accordion');

         accordionData.forEach(function (item) {
             var accordionItemHtml = createAccordionItem(item);
             accordionContainer.insertAdjacentHTML('beforeend', accordionItemHtml);
         });
     }

     // Call the function to populate the accordion when the page loads
     window.onload = populateAccordion;
     function reloadPage() {
         location.reload();
     }
 </script>
</asp:Content>
