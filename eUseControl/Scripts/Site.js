$(document).ready(function () {
     $(document).on('click', function (event) {
          var clickover = $(event.target);
          var opened = $('.navbar-collapse').hasClass('show');
          var navToggler = $('.navbar-toggler');
          if (opened === true && !clickover.hasClass('navbar-toggler') && clickover.parents('.navbar-collapse').length === 0) {
               navToggler.click();
          }
     });
});

///////////////////////////////////////////////////////// Delete User Confirmation Control for Admin/////////////////////////////////////////////////////////////////////////
function confirmDelete(userId) {
     document.querySelector('#deleteUserForm input[name="userId"]').value = userId;
     $('#deleteUserModal').modal('show');
}

///////////////////////////////////////////////////////// Modify User Control for Admin/////////////////////////////////////////////////////////////////////////
function openModifyModal(userId, firstName, lastName, userName, emailAddress, password, ip) {
     document.querySelector('#modifyUserForm input[name="userId"]').value = userId;
     document.querySelector('#modifyUserForm input[name="firstName"]').value = firstName;
     document.querySelector('#modifyUserForm input[name="lastName"]').value = lastName;
     document.querySelector('#modifyUserForm input[name="userName"]').value = userName;
     document.querySelector('#modifyUserForm input[name="emailAddress"]').value = emailAddress;
     document.querySelector('#modifyUserForm input[name="password"]').value = password;
     document.querySelector('#modifyUserForm input[name="ip"]').value = ip;
     $('#modifyUserModal').modal('show');
}