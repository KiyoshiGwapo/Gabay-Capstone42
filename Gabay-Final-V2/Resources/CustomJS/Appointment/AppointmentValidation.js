document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector('.guestMaster');

    const fullNameInput = form.querySelector('.FullName');
    const errorNames = form.querySelector('.nameError');

    const deptInput = form.querySelector('.departmentChoices');
    const errorDept = form.querySelector('.departmentError');

    const dateAppointment = form.querySelector('.appointmentDate');
    const errorDate = form.querySelector('.dateError');

    const timeAppointment = form.querySelector('.appointmentTime');
    const errorTime = form.querySelector('.timeError');

    const emailInput = form.querySelector('.Email');
    const errorEmail = form.querySelector('.emailError');

    const contactInput = form.querySelector('.ContactN');
    const errorContact = form.querySelector('.contactError');

    const appointmentConcern = form.querySelector('.Message');
    const concernError = form.querySelector('.concernError');

    function checkName() {
        const regex = /^[A-Za-z. ]+$/;

        if (!regex.test(fullNameInput.value) || fullNameInput.value === "") {
            fullNameInput.classList.add('is-invalid');
            fullNameInput.classList.remove('is-valid');
            errorNames.classList.remove('d-none');
            return false;
        } else {
            fullNameInput.classList.remove('is-invalid');
            fullNameInput.classList.add('is-valid');
            errorNames.classList.add('d-none');
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
    function checkDate() {
        //const regex = /\d/;

        if (dateAppointment.value === "" || dateAppointment.value == null) {
            dateAppointment.classList.add('is-invalid');
            dateAppointment.classList.remove('is-valid');
            errorDate.classList.remove('d-none');
            return false;
        } else {
            dateAppointment.classList.remove('is-invalid');
            dateAppointment.classList.add('is-valid');
            errorDate.classList.add('d-none');
            return true;
        }
    }
    function checkAppntTime() {

        if (timeAppointment.value === '' || timeAppointment == null) {
            timeAppointment.classList.add('is-invalid');
            timeAppointment.classList.remove('is-valid');
            errorTime.classList.remove('d-none');
            return false;
        } else {
            timeAppointment.classList.remove('is-invalid');
            timeAppointment.classList.add('is-valid');
            errorTime.classList.add('d-none');
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
        const regex = /^(09\d{2}|\(02\))\d{3}\d{4}$/;

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
    function checkConcern() {
        //const regex = /\d/;

        if (appointmentConcern.value === "") {
            appointmentConcern.classList.add('is-invalid');
            appointmentConcern.classList.remove('is-valid');
            concernError.classList.remove('d-none');
            return false;
        } else {
            appointmentConcern.classList.remove('is-invalid');
            appointmentConcern.classList.add('is-valid');
            concernError.classList.add('d-none');
            return true;
        }
    }



    form.addEventListener("submit", (e) => {

        if (!checkName()) {
            e.preventDefault();
        }

        if (!checkDepartment()) {
            e.preventDefault();
        }

        if (!checkDate()) {
            e.preventDefault();
        }

        if (!checkAppntTime()) {
            e.preventDefault();
        }

        if (!checkEmail()) {
            e.preventDefault();
        }

        if (!checkContact()) {
            e.preventDefault();
        }

        if (!checkConcern()) {
            e.preventDefault();
        }
    });

    fullNameInput.addEventListener('keyup', checkName);
    deptInput.addEventListener('change', checkDepartment);
    dateAppointment.addEventListener('change', checkDate);
    timeAppointment.addEventListener('change', checkAppntTime);
    emailInput.addEventListener('keyup', checkEmail);
    contactInput.addEventListener('keyup', checkContact);
    appointmentConcern.addEventListener('keyup', checkConcern);

});