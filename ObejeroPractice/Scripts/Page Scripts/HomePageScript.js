/*This is used in order for the jquery to execute only when
 the entire page of the view is fully loaded*/
$().ready(function () {

/*Create a click mouse event. this functions when the mouse clicks
 the submit button*/
    $('#submitData').click(function () {

        //get the inputed value from the view
        //create a variable
        var fname = $('#fnameData').val();
        var lname = $('#lnameData').val();
        var age = $('#ageData').val();

        //this is ajax
        $.post("../Home/SaveData1", { firstname: fname, lastname: lname, age1: age }, function (result) {

            if (result[0].value == 1) {
                alert("jqueryy success");
            }
            else {
                alert("jqueryy failed");
            }

        });
    });
});