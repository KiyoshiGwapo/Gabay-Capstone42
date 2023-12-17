document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector('.studentMaster');

    if (form) {
        const currentPassInput = form.querySelector('.currentPasswordTextBox');
        const errorCurrentPass = form.querySelector('.passwordCurrent');

        const newPassInput = form.querySelector('.newPasswordTextBox');
        const errorNewPass = form.querySelector('.passwordError');

        const cpassInput = form.querySelector('.confirmPasswordTextBox');
        const errorcPass = form.querySelector('.cpasswordError');

        const currentEmailInput = form.querySelector('.currentEmailTextBox');
        const errorEmail = form.querySelector('.emailError');

        const newEmailInput = form.querySelector('.newEmailTextBox');
        const errorNewEmail = form.querySelector('.newEmailError');

        function checkCurrentPass() {
            const regex = /^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()+=._-])[a-zA-Z0-9!@#$%^&*()+=._-]{8,}$/;

            if (!regex.test(currentPassInput.value) || currentPassInput.value === "") {
                currentPassInput.classList.add('is-invalid');
                currentPassInput.classList.remove('is-valid');
                errorCurrentPass.classList.remove('d-none');
                return false;
            } else {
                currentPassInput.classList.remove('is-invalid');
                currentPassInput.classList.add('is-valid');
                errorCurrentPass.classList.add('d-none');
                return true;
            }
        }
        function checkPassword() {
            const regex = /^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()+=._-])[a-zA-Z0-9!@#$%^&*()+=._-]{8,}$/;

            if (!regex.test(newPassInput.value) || newPassInput.value === "") {
                newPassInput.classList.add('is-invalid');
                newPassInput.classList.remove('is-valid');
                errorNewPass.classList.remove('d-none');
                return false;
            } else {
                newPassInput.classList.remove('is-invalid');
                newPassInput.classList.add('is-valid');
                errorNewPass.classList.add('d-none');
                return true;
            }
        }
        function checkCpasword() {
            if (newPassInput.value !== cpassInput.value || cpassInput.value === '') {
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
        function checkCurentEmail() {
            const regex = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

            if (!regex.test(currentEmailInput.value) || currentEmailInput.value === "") {
                currentEmailInput.classList.add('is-invalid');
                currentEmailInput.classList.remove('is-valid');
                errorEmail.classList.remove('d-none');
                return false;
            } else {
                currentEmailInput.classList.remove('is-invalid');
                currentEmailInput.classList.add('is-valid');
                errorEmail.classList.add('d-none');
                return true;
            }
        }
        function checkNewEmail() {
            const regex = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

            if (!regex.test(newEmailInput.value) || newEmailInput.value === "") {
                newEmailInput.classList.add('is-invalid');
                newEmailInput.classList.remove('is-valid');
                errorNewEmail.classList.remove('d-none');
                return false;
            } else {
                newEmailInput.classList.remove('is-invalid');
                newEmailInput.classList.add('is-valid');
                errorNewEmail.classList.add('d-none');
                return true;
            }
        }


        form.addEventListener("submit", (e) => {

            let isValid = true;

            if (!checkCurrentPass() || !checkPassword() || !checkCpasword()) {
                isValid = false;
            }

            if (!checkCuurentEmail() || !checkNewEmail()) {
                isValid = false;
            }

            if (!isValid) {
                e.preventDefault();
            }
        });

        currentPassInput.addEventListener('keyup', checkCurrentPass);
        newPassInput.addEventListener('keyup', checkPassword);
        cpassInput.addEventListener('keyup', checkCpasword);
        currentEmailInput.addEventListener('keyup', checkCurentEmail);
        newEmailInput.addEventListener('keyup', checkNewEmail);
    }
    
});