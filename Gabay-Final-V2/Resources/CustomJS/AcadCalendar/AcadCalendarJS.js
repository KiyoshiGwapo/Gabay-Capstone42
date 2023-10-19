document.addEventListener("DOMContentLoaded", () => {

    const uploadForm = document.querySelector('.adminMaster');

    const fileUpld = uploadForm.querySelector('.fileUpload');
    const errFileUpld = uploadForm.querySelector('.FileError');

    const fileName = uploadForm.querySelector('.fileName');
    const errFileName = uploadForm.querySelector('.nameError');

    function checkFile() {

        const regex = /\.pdf$/

        if (!regex.test(fileUpld.value) || fileUpld.value === "") {
            fileUpld.classList.add('is-invalid');
            fileUpld.classList.remove('is-valid');
            errFileUpld.classList.remove('d-none');
            return false;
        } else {
            fileUpld.classList.add('is-valid');
            fileUpld.classList.remove('is-invalid');
            errFileUpld.classList.add('d-none');
            return true;
        }
    }
    function checkFileName() {

        if (fileName.value === "" || fileName == null) {
            fileName.classList.add('is-invalid');
            fileName.classList.remove('is-valid');
            errFileName.classList.remove('d-none');
            return false;
        } else {
            fileName.classList.add('is-valid');
            fileName.classList.remove('is-invalid');
            errFileName.classList.add('d-none');
            return true;
        }
    }

    uploadForm.addEventListener("submit", (e) => {
        if (!checkFile()) {
            e.preventDefault();
        }
        if (!checkFileName()) {
            e.preventDefault();
        }
    });

    fileUpld.addEventListener('change', checkFile);
    fileName.addEventListener('keyup', checkFileName);
});
