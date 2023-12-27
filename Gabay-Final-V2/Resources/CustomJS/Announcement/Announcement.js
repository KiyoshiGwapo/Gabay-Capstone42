document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector('.departmentMaster');

    if (form) {
        const titleBxInput = form.querySelector('.addTitlebx');
        const errorTitle = form.querySelector('.annoucementitle');

        const datebxInput = form.querySelector('.addDatebx');
        const errorDatebx = form.querySelector('.addDatebxError');

        const startTimeInput = form.querySelector('.addStartTime');
        const endTimeInput = form.querySelector('.addEndTime');
        const errorTime = form.querySelector('.timeError');

        const addFileInput = form.querySelector('.addFilebx');
        const errorImg = form.querySelector('.imgError');

        const shrtdescInput = form.querySelector('.addShrtbx');
        const errorShrtDesc = form.querySelector('.shrtDescError');
        const errorMax = form.querySelector('.maxError');

        const lngdescInput = form.querySelector('.addDtldbx');
        const errorLngdesc = form.querySelector('.lngDescError');


        function checkTitle() {
            if (titleBxInput.value === '') {
                titleBxInput.classList.add('is-invalid');
                titleBxInput.classList.remove('is-valid');
                errorTitle.classList.remove('d-none');
                return false;
            } else {
                titleBxInput.classList.remove('is-invalid');
                titleBxInput.classList.add('is-valid');
                errorTitle.classList.add('d-none');
                return true;
            }
        }
        function checkDate() {
            if (datebxInput.value === '') {
                datebxInput.classList.add('is-invalid');
                datebxInput.classList.remove('is-valid');
                errorDatebx.classList.remove('d-none');
                return false;
            } else {
                datebxInput.classList.remove('is-invalid');
                datebxInput.classList.add('is-valid');
                errorDatebx.classList.add('d-none');
                return true;
            }
        }

        function checkTime() {
            const regex = /^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/;

            if (!regex.test(startTimeInput.value) || !regex.test(endTimeInput.value)) {
                startTimeInput.classList.add('is-invalid');
                endTimeInput.classList.add('is-invalid');
                errorTime.classList.remove('d-none');
                return false;
            } else {
                startTimeInput.classList.remove('is-invalid');
                endTimeInput.classList.remove('is-invalid');
                errorTime.classList.add('d-none');
                return true;
            }
        }

        function checkFileExtension() {
            const regex = /\.(jpe?g|png)$/i;

            if (!regex.test(addFileInput.value) || addFileInput.value === "") {
                addFileInput.classList.add('is-invalid');
                addFileInput.classList.remove('is-valid');
                errorImg.classList.remove('d-none');
                return false;
            } else {
                addFileInput.classList.remove('is-invalid');
                addFileInput.classList.add('is-valid');
                errorImg.classList.add('d-none');
                return true;
            }
        }
        function checkShrtdescInput() {
            if (shrtdescInput.value === '') {
                shrtdescInput.classList.add('is-invalid');
                shrtdescInput.classList.remove('is-valid');
                errorShrtDesc.classList.remove('d-none');
                return false;
            }else if (shrtdescInput.value.length > 50) {
                shrtdescInput.classList.add('is-invalid');
                shrtdescInput.classList.remove('is-valid');
                errorShrtDesc.classList.add('d-none');
                errorMax.classList.remove('d-none');
                return false;
            } else {
                shrtdescInput.classList.remove('is-invalid');
                shrtdescInput.classList.add('is-valid');
                errorShrtDesc.classList.add('d-none');
                errorMax.classList.add('d-none');
                return true;
            }
        }
        function checkLngdesc() {

            if (lngdescInput.value === "") {
                lngdescInput.classList.add('is-invalid');
                lngdescInput.classList.remove('is-valid');
                errorLngdesc.classList.remove('d-none');
                return false;
            } else {
                lngdescInput.classList.remove('is-invalid');
                lngdescInput.classList.add('is-valid');
                errorLngdesc.classList.add('d-none');
                return true;
            }
        }

        form.addEventListener("submit", (e) => {
            let isValid = true;

            if (!checkTitle()) {
                isValid = false;
            }

            if (!checkDate()) {
                isValid = false;
            }

            if (!checkTime()) {
                isValid = false;
            }

            if (!checkFileExtension()) {
                isValid = false;
            }

            if (!checkShrtdescInput()) {
                isValid = false;
            }

            if (!checkShrtdescInput()) {
                isValid = false;
            }

            if (!isValid) {
                e.checkLngdesc();
            }

        });

        titleBxInput.addEventListener('keyup', checkTitle);
        datebxInput.addEventListener('change', checkDate);
        startTimeInput.addEventListener('change', checkTime);
        endTimeInput.addEventListener('change', checkTime);
        addFileInput.addEventListener('change', checkFileExtension);
        shrtdescInput.addEventListener('keyup', checkShrtdescInput);
        lngdescInput.addEventListener('keyup', checkLngdesc);
    }
});