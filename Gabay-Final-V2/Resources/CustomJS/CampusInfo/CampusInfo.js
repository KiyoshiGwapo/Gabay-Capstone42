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