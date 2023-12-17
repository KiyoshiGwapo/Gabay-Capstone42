//const textBox = document.getElementById("<%= TextBox1.ClientID %>");
//        const errorText = document.getElementById("textError");

//        const textBox1 = document.getElementById("<%= TextBox2.ClientID %>");
//        const errorText1 = document.getElementById("textError1");

//        function checkTextbox() {
//            if (textBox.value.trim() === "") {
//                textBox.classList.add('is-invalid');
//                textBox.classList.remove('is-valid');
//                errorText.classList.remove('d-none');
//                return false;
//            } else {
//                textBox.classList.remove('is-invalid');
//                textBox.classList.add('is-valid');
//                errorText.classList.add('d-none');
//                return true;
//            }
//        }

//        function checkTextbox1() {
//            if (textBox1.value.trim() === "") {
//                textBox1.classList.add('is-invalid');
//                textBox1.classList.remove('is-valid');
//                errorText1.classList.remove('d-none');
//                return false;
//            } else {
//                textBox1.classList.remove('is-invalid');
//                textBox1.classList.add('is-valid');
//                errorText1.classList.add('d-none');
//                return true;
//            }
//        }

//        function validateButton1(e) {

//            if (!checkTextbox()) {
//                e.preventDefault();
//            }

//            if (!checkTextbox1()) {
//                e.preventDefault();
//            }
//        }

//        document.getElementById("Button1").addEventListener('click', validateButton1);
//        textBox.addEventListener('keyup', checkTextbox);
//textBox1.addEventListener('keyup', checkTextbox1);

//document.addEventListener("DOMContentLoaded", function () {
//    const form = document.querySelector('.form1');

//    const textBox = form.querySelector('.TextBox1');
//    const errorText = form.querySelector('.textError');

//    const textBox1 = form.querySelector('.TextBox2');
//    const errorText1 = form.querySelector('.textError1');

//    const button1 = form.querySelector('.Button1');

//    function checkTextbox() {
//        if (textBox.value.trim() === "") {
//            textBox.classList.add('is-invalid');
//            textBox.classList.remove('is-valid');
//            errorText.classList.remove('d-none');
//            return false;
//        } else {
//            textBox.classList.remove('is-invalid');
//            textBox.classList.add('is-valid');
//            errorText.classList.add('d-none');
//            return true;
//        }
//    }

//    function checkTextbox1() {
//        if (textBox1.value.trim() === "") {
//            textBox1.classList.add('is-invalid');
//            textBox1.classList.remove('is-valid');
//            errorText1.classList.remove('d-none');
//            return false;
//        } else {
//            textBox1.classList.remove('is-invalid');
//            textBox1.classList.add('is-valid');
//            errorText1.classList.add('d-none');
//            return true;
//        }
//    }

//    function validateButton1() {
//        const isTextBoxValid = checkTextbox();
//        const isTextBox1Valid = checkTextbox1();

//        // Disable Button1 if either TextBox1 or TextBox2 is not completed
//        button1.disabled = !(isTextBoxValid && isTextBox1Valid);

//        return isTextBoxValid && isTextBox1Valid;
//    }

//    textBox.addEventListener('keyup', validateButton1);
//    textBox1.addEventListener('keyup', validateButton1);
//});

document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector('.form1');

    const nameInput = form.querySelector('.TextBox1');
    const errorName = form.querySelector('.textError');

    const nameInput1 = form.querySelector('.TextBox2');
    const errorlAddress = form.querySelector('.textError1');

    function checkName() {
        const regex = /\d/;

        if (regex.test(nameInput.value) || nameInput.value === "") {
            nameInput.classList.add('is-invalid');
            nameInput.classList.remove('is-valid');
            errorName.classList.remove('d-none');
            return false;
        } else {
            nameInput.classList.remove('is-invalid');
            nameInput.classList.add('is-valid');
            errorName.classList.add('d-none');
            return true;
        }
    }
    function checkName1() {
        const regex = /\d/;

        if (regex.test(nameInput1.value) || nameInput1.value === "") {
            nameInput1.classList.add('is-invalid');
            nameInput1.classList.remove('is-valid');
            errorlAddress.classList.remove('d-none');
            return false;
        } else {
            nameInput1.classList.remove('is-invalid');
            nameInput1.classList.add('is-valid');
            errorlAddress.classList.add('d-none');
            return true;
        }
    }

    form.addEventListener("submit", (e) => {
        if (!checkName()) {
            e.preventDefault();
        }
        if (!checkName1()) {
            e.preventDefault();
        }
    });

    nameInput.addEventListener('keyup', checkName);
    nameInput1.addEventListener('keyup', checkName1);
});