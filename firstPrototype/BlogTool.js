/*
$(document).ready(function() {
	
	$('.menu-trigger').click(function() {
		
		$(".menu").fadeToggle();
	});
	
	$(window).resize(function() {

		$(".menu").removeAttr('style');
	});

});


$(document).ready(function() {
	
	$('.login-btn').click(function() {
		
		$('.login').css({'display': 'block'});
		$(".login").fadeToggle();
		
	});
	
	
	$(window).resize(function() {
		$(".login-close-btn").removeAttr('style');
	});
	

});

*/
$(document).ready(function() {

  $('.menu-trigger').click(function() {
    $(".menu").fadeToggle();    
  });
  
  $(window).resize(function() {
    $(".menu").removeAttr('style');
  });

});