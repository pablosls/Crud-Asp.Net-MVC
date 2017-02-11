
$(document).ready(function () {

    // Botao Consultar
    $("#btnConsultar").click(function (event) {
        var codigo = $("#CodMatricula").val();
        if (codigo != "") {
            window.location.href = "/Sistema/Editar/" + codigo;
        }
        else {
            alertify.alert('Minha janela', 'Informe o Código do Aluno que deseja consultar!');
        }
        event.preventDefault();
    });

    //Botão Excluir
    $("#btnExcluir").click(function () {      
        alertify.dialog('confirm').set({
              'title': 'Atenção',
              'labels': { ok: 'Confirmar!', cancel: 'Cancelar!' },
              'message': '<b>Deseja realmente excluir?</b>',
              'onok': function () {
                  var codigo = $("#btnExcluir").attr('codigo');
                  window.location = "/Sistema/ExcluirAluno/" + codigo;
              },
              'oncancel': function () {
                 

                  alertify.notify('Exclusão cancelada', 'error', 5, function () { console.log('dismissed'); });
              }
          }).show();
    });

    // Botao Novo
    $('#btnNovo').click(function () {
        window.location.href = "/Sistema/Cadastro";
    });

    // Botao Importar
    $('#btnImportarXML').click(function () {
        window.location.href = "/Sistema/importaXml";
    });

    //Idade
    $('#Idade').prop('readonly', true);

    // Codigo 
    if ($("#btnExcluir").size()) {
        $('#CodMatricula').prop('readonly', true);
    };

    //Calcular Idade
    $('#DataNascimento').change(function () {
        var NovaData = $('#DataNascimento').val();
        if(isNaN(NovaData)){
            $('#Idade').val(calculaIdade());
        }
        
    });

});



function calculaIdade() {
    var dtNascimento = $('#DataNascimento').val();
    var dataObj = new Date(dtNascimento);
    var dataAtual = new Date();
    var anoAtual = dataAtual.getFullYear();
    var birthdayThisYear = new Date(anoAtual, dataObj.getMonth(), dataObj.getDate());
    var idade = anoAtual - dataObj.getFullYear();
    if (birthdayThisYear > dataAtual) {
        idade--;
    }
    return idade;
}
 
 



