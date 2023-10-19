document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector('.departmentMaster');

    const lgnID = form.querySelector('.lgnIDBx');
    const lgnError = form.querySelector('.errorLgn');

    const psInput = form.querySelector('.pssBx');
    const psError = form.querySelector('.errorPass');

    const deptDescInpt = form.querySelector('.deptDescription');
    const depdescErr = form.querySelector('.errDeptName');

    const deptNameInpt = form.querySelector('.deptNameBx');
    const deptNameErr = form.querySelector('.errDeptName');

    const deptDeanInpt = form.querySelector('.deptHeadBx');
    const deptDeanErr = form.querySelector('.errDeptHead');

    const offhrsInpt = form.querySelector('.officeHour');
    const offhrsErr = form.querySelector('.errOffHrs');

    const deptEmail = form.querySelector('.emailTxtBx');
    const deptEmailErr = form.querySelector('.errEmail');

    const deptConNum = form.querySelector('.contactNumber');
    const deptConNumErr = form.querySelector('.errConNum');

    const deptCourse = form.querySelector('.CoursesAppended');
    const deptCourseErr = form.querySelector('.errCourse');

    function checkLoginID() {
        //const regex = /\d/;

        if (lgnID.value === "") {
            lgnID.classList.add('is-invalid');
            lgnID.classList.remove('is-valid');
            lgnError.classList.remove('d-none');
            return false;
        } else {
            lgnID.classList.remove('is-invalid');
            lgnID.classList.add('is-valid');
            lgnError.classList.add('d-none');
            return true;
        }
    }
    function checkPassword() {
        const regex = /^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()+=._-])[a-zA-Z0-9!@#$%^&*()+=._-]{8,}$/;

        if (!regex.test(psInput.value) || psInput.value === "") {
            psInput.classList.add('is-invalid');
            psInput.classList.remove('is-valid');
            psError.classList.remove('d-none');
            return false;
        } else {
            psInput.classList.remove('is-invalid');
            psInput.classList.add('is-valid');
            psError.classList.add('d-none');
            return true;
        }
    }
    function checkDeptDesc() {

        if (deptDescInpt.value === "") {
            deptDescInpt.classList.add('is-invalid');
            deptDescInpt.classList.remove('is-valid');
            depdescErr.classList.remove('d-none');
            return false;
        } else {
            deptDescInpt.classList.remove('is-invalid');
            deptDescInpt.classList.add('is-valid');
            depdescErr.classList.add('d-none');
            return true;
        }
    }
    function checkDeptName() {
        const regex = /\d/;

        if (regex.test(deptNameInpt.value) || deptNameInpt.value === "") {
            deptNameInpt.classList.add('is-invalid');
            deptNameInpt.classList.remove('is-valid');
            deptNameErr.classList.remove('d-none');
            return false;
        } else {
            deptNameInpt.classList.remove('is-invalid');
            deptNameInpt.classList.add('is-valid');
            deptNameErr.classList.add('d-none');
            return true;
        }
    }
    function checkDeptDean() {
        const regex = /\d/;

        if (regex.test(deptDeanInpt.value) || deptDeanInpt.value === "") {
            deptDeanInpt.classList.add('is-invalid');
            deptDeanInpt.classList.remove('is-valid');
            deptDeanErr.classList.remove('d-none');
            return false;
        } else {
            deptDeanInpt.classList.remove('is-invalid');
            deptDeanInpt.classList.add('is-valid');
            deptDeanErr.classList.add('d-none');
            return true;
        }
    }
    function checkOffHrs() {

        if (offhrsInpt.value === "") {
            offhrsInpt.classList.add('is-invalid');
            offhrsInpt.classList.remove('is-valid');
            offhrsErr.classList.remove('d-none');
            return false;
        } else {
            offhrsInpt.classList.remove('is-invalid');
            offhrsInpt.classList.add('is-valid');
            offhrsErr.classList.add('d-none');
            return true;
        }
    }
    function checkConNum() {
        const regex = /\d/;

        if (!regex.test(deptConNum.value) || deptConNum.value === "") {
            deptConNum.classList.add('is-invalid');
            deptConNum.classList.remove('is-valid');
            deptConNumErr.classList.remove('d-none');
            return false;
        } else {
            deptConNum.classList.remove('is-invalid');
            deptConNum.classList.add('is-valid');
            deptConNumErr.classList.add('d-none');
            return true;
        }
    }
    function checkdeptEmail() {
        const regex = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

        if (!regex.test(deptEmail.value) || deptEmail.value === "") {
            deptEmail.classList.add('is-invalid');
            deptEmail.classList.remove('is-valid');
            deptEmailErr.classList.remove('d-none');
            return false;
        } else {
            deptEmail.classList.remove('is-invalid');
            deptEmail.classList.add('is-valid');
            deptEmailErr.classList.add('d-none');
            return true;
        }
    }
    function checkCourses() {

        if (deptCourse.value === "") {
            deptCourse.classList.add('is-invalid');
            deptCourse.classList.remove('is-valid');
            deptCourseErr.classList.remove('d-none');
            return false;
        } else {
            deptCourse.classList.remove('is-invalid');
            deptCourse.classList.add('is-valid');
            deptCourseErr.classList.add('d-none');
            return true;
        }
    }

    form.addEventListener("submit", (e) => {
        if (!checkLoginID()) {
            e.preventDefault();
        }
        if (!checkPassword()) {
            e.preventDefault();
        }
        if (!checkDeptDesc()) {
            e.preventDefault();
        }
        if (!checkDeptName()) {
            e.preventDefault();
        }
        if (!checkDeptDean()) {
            e.preventDefault();
        }
        if (!checkOffHrs()) {
            e.preventDefault();
        }
        if (!checkConNum()) {
            e.preventDefault();
        }
        if (!checkdeptEmail()) {
            e.preventDefault();
        }
        if (!checkCourses()) {
            e.preventDefault();
        }
    });

    lgnID.addEventListener('input', checkLoginID);
    psInput.addEventListener('input', checkPassword);
    deptDescInpt.addEventListener('input', checkDeptDesc);
    deptNameInpt.addEventListener('input', checkDeptName);
    deptDeanInpt.addEventListener('input', checkDeptDean);
    offhrsInpt.addEventListener('input', checkOffHrs);
    deptConNum.addEventListener('input', checkConNum);
    deptEmail.addEventListener('input', checkdeptEmail);
    deptCourse.addEventListener('input', checkCourses);
});
