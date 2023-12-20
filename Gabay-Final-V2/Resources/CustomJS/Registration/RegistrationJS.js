document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector('.form1');

    const nameInput = form.querySelector('.name');
    const errorName = form.querySelector('.nameError');

    const addressInput = form.querySelector('.address');
    const errorlAddress = form.querySelector('.addressError');

    const contactInput = form.querySelector('.contact');
    const errorContact = form.querySelector('.contactError');

    const DOBInput = form.querySelector('.DOB');
    const errorDOB = form.querySelector('.DOBError');

    const passInput = form.querySelector('.password');
    const errorPass = form.querySelector('.passError');

    const cpassInput = form.querySelector('.cpassword');
    const errorcPass = form.querySelector('.cpassError');

    const emailInput = form.querySelector('.email');
    const errorEmail = form.querySelector('.emailError');

    const idNumberInput = form.querySelector('.idNumber');
    const errorIdNum = form.querySelector('.idNumError');

    const deptInput = form.querySelector('.department');
    const errorDept = form.querySelector('.departmentError');

    const courseInput = form.querySelector('.course');
    const errorCourse = form.querySelector('.courseError');

    const yearInput = form.querySelector('.courseYear');
    const errorYear = form.querySelector('.courseYearError');

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
    function checkAddress() {
        //const regex = /\d/;

        if (addressInput.value === "") {
            addressInput.classList.add('is-invalid');
            addressInput.classList.remove('is-valid');
            errorlAddress.classList.remove('d-none');
            return false;
        } else {
            addressInput.classList.remove('is-invalid');
            addressInput.classList.add('is-valid');
            errorlAddress.classList.add('d-none');
            return true;
        }
    }
    function checkContact() {
        const regex = /\d/;

        if (!regex.test(contactInput.value) || contactInput.value === "") {
            contactInput.classList.add('is-invalid');
            contactInput.classList.remove('is-valid');
            errorContact.classList.remove('d-none');
            return false;
        } else {
            contactInput.classList.remove('is-invalid');
            contactInput.classList.add('is-valid');
            errorContact.classList.add('d-none');
            return true;
        }
    }
    function checkDOB() {
        //const regex = /\d/;

        if (DOBInput.value === "" || DOBInput.value == null) {
            DOBInput.classList.add('is-invalid');
            DOBInput.classList.remove('is-valid');
            errorDOB.classList.remove('d-none');
            return false;
        } else {
            DOBInput.classList.remove('is-invalid');
            DOBInput.classList.add('is-valid');
            errorDOB.classList.add('d-none');
            return true;
        }
    }
    function checkPassword() {
        const regex = /^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()+=._-])[a-zA-Z0-9!@#$%^&*()+=._-]{8,}$/;

        if (!regex.test(passInput.value) || passInput.value === "") {
            passInput.classList.add('is-invalid');
            passInput.classList.remove('is-valid');
            errorPass.classList.remove('d-none');
            return false;
        } else {
            passInput.classList.remove('is-invalid');
            passInput.classList.add('is-valid');
            errorPass.classList.add('d-none');
            return true;
        }
    }
    function checkCpasword() {
        if (passInput.value !== cpassInput.value || cpassInput.value === '') {
            cpassInput.classList.add('is-invalid');
            cpassInput.classList.remove('is-valid');
            errorcPass.classList.remove('d-none');
            return false;
        } else {
            cpassInput.classList.remove('is-invalid');
            cpassInput.classList.add('is-valid');
            errorcPass.classList.add('d-none');
            return true;
        }
    }
    function checkEmail() {
        const regex = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

        if (!regex.test(emailInput.value) || emailInput.value === "") {
            emailInput.classList.add('is-invalid');
            emailInput.classList.remove('is-valid');
            errorEmail.classList.remove('d-none');
            return false;
        } else {
            emailInput.classList.remove('is-invalid');
            emailInput.classList.add('is-valid');
            errorEmail.classList.add('d-none');
            return true;
        }
    }
    function checkIdnumber() {
        const regex = /^-?\d+$/;

        if (!regex.test(idNumberInput.value) || idNumberInput.value === "" || idNumberInput.value == null) {
            idNumberInput.classList.add('is-invalid');
            idNumberInput.classList.remove('is-valid');
            errorIdNum.classList.remove('d-none');
            return false;
        } else {
            idNumberInput.classList.remove('is-invalid');
            idNumberInput.classList.add('is-valid');
            errorIdNum.classList.add('d-none');
            return true;
        }
    }
    function checkDepartment() {

        if (deptInput.value === '' || deptInput == null) {
            deptInput.classList.add('is-invalid');
            deptInput.classList.remove('is-valid');
            errorDept.classList.remove('d-none');
            return false;
        } else {
            deptInput.classList.remove('is-invalid');
            deptInput.classList.add('is-valid');
            errorDept.classList.add('d-none');
            return true;
        }
    }
    function checkCourse() {
        if (courseInput.value === '' || courseInput == null) {
            courseInput.classList.add('is-invalid');
            courseInput.classList.remove('is-valid');
            errorCourse.classList.remove('d-none');
            return false;
        } else {
            courseInput.classList.remove('is-invalid');
            courseInput.classList.add('is-valid');
            errorCourse.classList.add('d-none');
            return true;
        }
    }
    function checkCourseYear() {

        if (yearInput.value === '' || yearInput.value == null) {
            yearInput.classList.add('is-invalid');
            yearInput.classList.remove('is-valid');
            errorYear.classList.remove('d-none');
            return false;
        } else {
            yearInput.classList.remove('is-invalid');
            yearInput.classList.add('is-valid');
            errorYear.classList.add('d-none');
            return true;
        }
    }

    form.addEventListener("submit", (e) => {
        if (!checkName()) {
            e.preventDefault();
        }

        if (!checkAddress()) {
            e.preventDefault();
        }

        if (!checkContact()) {
            e.preventDefault();
        }

        if (!checkDOB()) {
            e.preventDefault();
        }

        if (!checkPassword()) {
            e.preventDefault();
        }

        if (!checkCpasword()) {
            e.preventDefault();
        }

        if (!checkEmail()) {
            e.preventDefault();
        }

        if (!checkIdnumber()) {
            e.preventDefault();
        }

        if (!checkDepartment()) {
            e.preventDefault();
        }

        if (!checkCourseYear()) {
            e.preventDefault();
        }
        if (!checkCourse()) {
            e.preventDefault();
        }

    });
    nameInput.addEventListener('keyup', checkName);
    addressInput.addEventListener('keyup', checkAddress);
    contactInput.addEventListener('keyup', checkContact);
    DOBInput.addEventListener('change', checkDOB);
    passInput.addEventListener('keyup', checkPassword);
    cpassInput.addEventListener('keyup', checkCpasword);
    emailInput.addEventListener('keyup', checkEmail);
    idNumberInput.addEventListener('keyup', checkIdnumber);
    deptInput.addEventListener('change', checkDepartment);
    courseInput.addEventListener('change', checkCourse);
    yearInput.addEventListener('change', checkCourseYear);

});