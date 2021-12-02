$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resposta = confirm("Tem certeza que deseja realizar essa operação?");

        if (resposta == false){
            e.preventDefault();
        }
    });
    $('.dinheiro').mask('000.000.000.000.000,00', { reverse: true });

    AjaxUploadImagemProduto();
});

function AjaxUploadImagemProduto() {
    $(".img-upload").click(function () {
        $(this).parent().parent().find(".input-file").click();
    });

    $(".btn-imagem-excluir").click(function () {
        var CampoHidden = $(this).parent().find("input[name=imagem]");
        var Imagem = $(this).parent().find(".img-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagem-excluir");
        var InputFile = $(this).parent().find(".input-file");


        $.ajax({
            type: "GET",
            url: "/Colaborador/Imagem/Deletar?caminho=" + CampoHidden.val(),
            error: function () {
                //alert("Erro no envio do arquivo");
                //Imagem.attr("src", "/img/produto-padrao.png");
            },
            success: function () {
                Imagem.attr("src", "/img/produto-padrao.png");
                BtnExcluir.addClass("btn-ocultar");
                CampoHidden.val("");
                InputFile.val("");
            }
        });
    });

    $(".input-file").change(function () {
        //Formulário de dados via JavaScript
        var Binario = $(this)[0].files[0];
        var Formulario = new FormData();
        Formulario.append("file", Binario);

        var CampoHidden = $(this).parent().find("input[name=imagem]");
        var Imagem = $(this).parent().find(".img-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagem-excluir");

        //Apresenta Imagem Loading.
        Imagem.attr("src", "/img/loading.gif");
        Imagem.addClass("thumb");

        //TODO - Requisição Ajax enviado a Formulario
        $.ajax({
            type: "POST",
            url: "/Colaborador/Imagem/Armazenar",
            data: Formulario,
            contentType: false,
            processData: false,
            error: function () {
                alert("Erro no envio do arquivo!");
                Imagem.attr("src", "/img/produto-padrao.png");
                Imagem.removeClass("thumb");
            },
            success: function (data) {
                var Caminho = data.caminho;
                Imagem.attr("src", Caminho);
                Imagem.removeClass("thumb");
                CampoHidden.val(Caminho);
                BtnExcluir.removeClass("btn-ocultar");
            }
        });
    });
}