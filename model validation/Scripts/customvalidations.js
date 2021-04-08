/// <reference path="jquery.validate.js" />  
/// <reference path="jquery.validate.unobtrusive.js" />  
$.validator.unobtrusive.adapters.addSingleVal("exclude", "chars");
$.validator.addMethod("exclude", function (value, element, exclude) {
    if (value) {
        for (var i = 0; i < exclude.length; i++) {
            if (jQuery.inArray(exclude[i], value) != -1) {
                return false;
            }
        }
    }
    return true;
}); 

$.validator.unobtrusive.adapters.add("pastdate", {}, function (options) {
    options.rules['greaterThan'] = true;
    options.messages['greaterThan'] = options.message;
});

$.validator.addMethod("greaterThan", function (value, element, params) {
    var valid = false;

    if (!/Invalid|NaN/.test(new Date(value))) {
        valid = new Date(value) < new Date();;
    } 
    
    return valid;
}); 