const AUTH_SETTINGS = {
    PASSWORD_LENGTH: 6,
    EMAIL_REGEXP: /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/iu,
};

AUTH_SETTINGS.PASSWORD_REGEXP = new RegExp(`^.{${AUTH_SETTINGS.PASSWORD_LENGTH},}$`)

document.forms.authForm.addEventListener("submit", async e => {
    e.preventDefault();

    const login = document.forms.authForm.Email.value;
    const password = document.forms.authForm.Password.value;

    if (!isAuthValid(login, password)) {

        return;
    }

    var returnUrl = window.location.pathname + window.location.search;
    var fullActionUrl = e.submitter.getAttribute('data-action') + '?returnUrl=' + encodeURIComponent(returnUrl);

    const response = await fetch(fullActionUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Email: login,
            Password: password
        })
    });

    if (response.ok) {
        window.location.href = returnUrl

    } else {
        document.getElementById('authErrors').textContent = await response.text();
    }

});

/**
 * Валидация логина и пароля
 * @param {string} login
 * @param {string} password
 * @returns {boolean}
 */
function isAuthValid(login, password) {
    let isValid = true;

    clearError('Email');
    clearError('Password');

    if (!AUTH_SETTINGS.EMAIL_REGEXP.test(login)) {
        showError('Email', 'Неверный формат email');
        isValid = false;
    }

    if (!AUTH_SETTINGS.PASSWORD_REGEXP.test(password)) {
        showError('Password', `Пароль должен содержать как минимум ${AUTH_SETTINGS.PASSWORD_LENGTH} символов`);
        isValid = false;
    }

    return isValid;
}

/**
 * Отображение сообщения об ошибке для указанного поля
 * @param {string} fieldId - ID поля
 * @param {string} message - Сообщение об ошибке
 */
function showError(fieldId, message) {
    const field = document.getElementById(fieldId);
    const errorSpan = field.nextElementSibling;
    errorSpan.innerText = message;
}

/**
 * Очистка сообщения об ошибке для указанного поля
 * @param {string} fieldId - ID поля
 */
function clearError(fieldId) {
    const field = document.getElementById(fieldId);
    const errorSpan = field.nextElementSibling;
    errorSpan.innerText = '';
}
