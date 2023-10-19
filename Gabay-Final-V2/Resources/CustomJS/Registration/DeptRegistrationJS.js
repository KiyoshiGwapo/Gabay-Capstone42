document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector('.adminMaster');

    const deptLoginID = form.querySelector('.loginID');
    const errorDeptLoginID = form.querySelector('.loginIDError');

    const deptPass = form.querySelector('.loginPass');
    const errorDeptPass = form.querySelector('.passError');

    const deptName = form.querySelector('.DeptName');
    const errorDeptName = form.querySelector('.deptNameError');

    const deptHead = form.querySelector('.deptHead');
    const deptHeadError = form.querySelector('.deptHeadError');

    const deptDesc = form.querySelector('.deptDesc');
    const deptDescError = form.querySelector('.deptDescError');

    const deptEmail = form.querySelector('.deptEmail');
    const deptEmailError = form.querySelector('.deptEmailError');

    const deptConNum = form.querySelector('.deptCN');
    const deptConNumError = form.querySelector('.deptCNError');

    const deptOffHr = form.querySelector('.deptHours');
    const deptOffHrErr = form.querySelector('.deptHoursError');

    function checkLoginID() {
        /*const regex = /\d/;*/

        if (deptLoginID.value == "") {
            deptLoginID.classList.add('is-invalid');
            deptLoginID.classList.remove('is-valid');
            errorDeptLoginID.classList.remove('d-none');
            return false;
        } else {
            deptLoginID.classList.remove('is-invalid');
            deptLoginID.classList.add('is-valid');
            errorDeptLoginID.classList.add('d-none');
            return true;
        }
    }
    function checkDeptPass() {
        const regex = /^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()+=._-])[a-zA-Z0-9!@#$%^&*()+=._-]{8,}$/;

        if (!regex.test(deptPass.value) || deptPass.value === "") {
            deptPass.classList.add('is-invalid');
            deptPass.classList.remove('is-valid');
            errorDeptPass.classList.remove('d-none');
            return false;
        } else {
            deptPass.classList.remove('is-invalid');
            deptPass.classList.add('is-valid');
            errorDeptPass.classList.add('d-none');
            return true;
        }
    }
    function checkDeptName() {
        const regex = /\d/;

        if (regex.test(deptName.value) || deptName.value === "") {
            deptName.classList.add('is-invalid');
            deptName.classList.remove('is-valid');
            errorDeptName.classList.remove('d-none');
            return false;
        } else {
            deptName.classList.remove('is-invalid');
            deptName.classList.add('is-valid');
            errorDeptName.classList.add('d-none');
            return true;
        }
    }
    function checkDeanName() {
        const regex = /\d/;

        if (regex.test(deptHead.value) || deptHead.value === "") {
            deptHead.classList.add('is-invalid');
            deptHead.classList.remove('is-valid');
            deptHeadError.classList.remove('d-none');
            return false;
        } else {
            deptHead.classList.remove('is-invalid');
            deptHead.classList.add('is-valid');
            deptHeadError.classList.add('d-none');
            return true;
        }
    }
    function checkDeptDesc() {
        if (deptDesc.value == null || deptDesc.value === "") {
            deptDesc.classList.add('is-invalid');
            deptDesc.classList.remove('is-valid');
            deptDescError.classList.remove('d-none');
            return false;
        } else {
            deptDesc.classList.remove('is-invalid');
            deptDesc.classList.add('is-valid');
            deptDescError.classList.add('d-none');
            return true;
        }
    }
    function checkDeptEmail() {
        const regex = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

        if (!regex.test(deptEmail.value) || deptEmail.value === "") {
            deptEmail.classList.add('is-invalid');
            deptEmail.classList.remove('is-valid');
            deptEmailError.classList.remove('d-none');
            return false;
        } else {
            deptEmail.classList.remove('is-invalid');
            deptEmail.classList.add('is-valid');
            deptEmailError.classList.add('d-none');
            return true;
        }
    }
    function checkDeptCN() {
        const regex = /\d/;

        if (!regex.test(deptConNum.value) || deptConNum.value === "") {
            deptConNum.classList.add('is-invalid');
            deptConNum.classList.remove('is-valid');
            deptConNumError.classList.remove('d-none');
            return false;
        } else {
            deptConNum.classList.remove('is-invalid');
            deptConNum.classList.add('is-valid');
            deptConNumError.classList.add('d-none');
            return true;
        }
    }
    function checkDeptHour() {
        if (deptOffHr.value == null || deptOffHr.value === "") {
            deptOffHr.classList.add('is-invalid');
            deptOffHr.classList.remove('is-valid');
            deptOffHrErr.classList.remove('d-none');
            return false;
        } else {
            deptOffHr.classList.remove('is-invalid');
            deptOffHr.classList.add('is-valid');
            deptOffHrErr.classList.add('d-none');
            return true;
        }
    }

    form.addEventListener("submit", (e) => {
        if (!checkLoginID()) {
            e.preventDefault();
        }
        if (!checkDeptPass()) {
            e.preventDefault();
        }
        if (!checkDeptName()) {
            e.preventDefault();
        }
        if (!checkDeanName()) {
            e.preventDefault();
        }
        if (!checkDeptDesc()) {
            e.preventDefault();
        }
        if (!checkDeptEmail()) {
            e.preventDefault();
        }
        if (!checkDeptCN()) {
            e.preventDefault();
        }
        if (!checkDeptHour()) {
            e.preventDefault();
        }
    });
    deptLoginID.addEventListener('keyup', checkLoginID);
    deptPass.addEventListener('keyup', checkDeptPass);
    deptName.addEventListener('keyup', checkDeptName);
    deptHead.addEventListener('keyup', checkDeanName);
    deptDesc.addEventListener('keyup', checkDeptDesc);
    deptEmail.addEventListener('keyup', checkDeptEmail);
    deptConNum.addEventListener('keyup', checkDeptCN);
    deptOffHr.addEventListener('keyup', checkDeptHour);
});
