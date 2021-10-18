$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resposta = confirm("Tem certeza que deseja realizar essa operação?");

        if (resposta == false)
        {
            e.preventDefault();
        }            
    });
});