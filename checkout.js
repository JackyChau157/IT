var validNavigation = false;
function wireUpEvents() {

	dont_confirm_leave = 0;
	leave_message = "You sure you want to leave ?";

	function goodbye(e) {
		if (!validNavigation) {
			if (dont_confirm_leave !== 1) {
				if (!e)
					e = window.event;
				e.cancelBubble = true;
				// e.stopPropagation works in Firefox.
				if (e.stopPropagation) {
					e.stopPropagation();
					e.preventDefault();
				}
				// return works for Chrome and Safari
				return leave_message;
			}
		}
	}

	window.onbeforeunload = goodbye;

	document.onkeydown = function() {
		switch (event.keyCode || e.which) {
		case 116: // F5 button
			validNavigation = true;
		case 114: // F5 button
			validNavigation = true;
		case 82: // R button
			if (event.ctrlKey) {
				validNavigation = true;
			}
		case 13: // Press enter
			validNavigation = true;
		}
	};
	
	// Attach the event click for all links in the page
	$("a").bind("click", function() {
		validNavigation = true;
	});

	// Attach the event submit for all forms in the page
	$("form").bind("submit", function() {
		validNavigation = true;
	});

	// Attach the event click for all inputs in the page
	$("input[type=submit]").bind("click", function() {
		validNavigation = true;
	});
}

// Wire up the events as soon as the DOM tree is ready
$(document).ready(function() {
	wireUpEvents();
});

