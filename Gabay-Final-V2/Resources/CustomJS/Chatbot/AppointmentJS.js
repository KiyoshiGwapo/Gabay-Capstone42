document.addEventListener("DOMContentLoaded", () => {

    const form = document.querySelector('.guestMaster');

    const department = form.querySelector('.departmentChoices');
    const errorDept = form.querySelector('.departmentError');

    const nameInput = form.querySelector('.FullName');
    const errorName = form.querySelector('.nameError');

    const emailInput = form.querySelector('.Email');
    const errorEmail = form.querySelector('.emailError');

    const contactInput = form.querySelector('.ContactN');
    const errorContact = form.querySelector('.contactError');

    const AppointmentTime = form.querySelector('.time');
    const errorTime = form.querySelector('.timeError');

    const AppointmentDate = form.querySelector('.date');
    const errorDate = form.querySelector('.dateError');

    const message = form.querySelector('.Message');
    const errorConcern = form.querySelector('.concernError');

    function checkDepartment() {

        if (department.value === '' || department == null) {
            department.classList.add('is-invalid');
            department.classList.remove('is-valid');
            errorDept.classList.remove('d-none');
            return false;
        } else {
            deptInput.classList.remove('is-invalid');
            deptInput.classList.add('is-valid');
            errorDept.classList.add('d-none');
            return true;
        }
    }
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
    function checkTimet() {

        if (AppointmentTime.value === '' || AppointmentTime == null) {
            AppointmentTime.classList.add('is-invalid');
            AppointmentTime.classList.remove('is-valid');
            errorTime.classList.remove('d-none');
            return false;
        } else {
            AppointmentTime.classList.remove('is-invalid');
            AppointmentTime.classList.add('is-valid');
            errorTime.classList.add('d-none');
            return true;
        }
    }
    function checkDate() {

        if (AppointmentDate.value === '' || AppointmentDate == null) {
            AppointmentDate.classList.add('is-invalid');
            AppointmentDate.classList.remove('is-valid');
            errorDate.classList.remove('d-none');
            return false;
        } else {
            AppointmentDate.classList.remove('is-invalid');
            AppointmentDate.classList.add('is-valid');
            errorDate.classList.add('d-none');
            return true;
        }
    }
    function checkConcern() {
        //const regex = /\d/;

        if (message.value === "") {
            message.classList.add('is-invalid');
            message.classList.remove('is-valid');
            errorConcern.classList.remove('d-none');
            return false;
        } else {
            message.classList.remove('is-invalid');
            message.classList.add('is-valid');
            errorConcern.classList.add('d-none');
            return true;
        }
    }

    form.addEventListener("submit", (e) => {
       
        if (!checkDepartment()) {
            e.preventDefault();
        }

        if (!checkName()) {
            e.preventDefault();
        }

        if (!checkEmail()) {
            e.preventDefault();
        }

        if (!checkContact()) {
            e.preventDefault();
        }

        if (!checkTimet()) {
            e.preventDefault();
        }

        if (!checkDate()) {
            e.preventDefault();
        }

        if (!checkConcern()) {
            e.preventDefault();
        }
    });

    deptInput.addEventListener('change', checkDepartment);
    nameInput.addEventListener('keyup', checkName);
    emailInput.addEventListener('keyup', checkEmail);
    contactInput.addEventListener('keyup', checkContact);
    AppointmentTime.addEventListener('change', checkDepartment);
    AppointmentDate.addEventListener('keyup', checkDepartment);
    message.addEventListener('keyup', checkName);

});