const sidebar = document.querySelector('.sidebar');
const toggleBtn = document.querySelector('.toggle-btn');

const popupBox = document.querySelector('.popup-box');
const showPopup = document.querySelector('.show-popup');
const closePopup = document.querySelector('.btn-close');

toggleBtn.addEventListener('click', () => {
    sidebar.classList.toggle('active');
});


showPopup.addEventListener('click', () => {
    popupBox.classList.add('active');
});

closePopup.addEventListener('click', () => {
    popupBox.classList.remove('active');
});