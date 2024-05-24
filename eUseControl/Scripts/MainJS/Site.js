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