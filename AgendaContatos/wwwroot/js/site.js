$(document).ready(function () {
  setTimeout(function () {
    initializeDataTables();
  });

  $('.btn-total-contatos').click(function () {
    const usuarioId = $(this).attr('usuario-id');
    fetchContatosPorUsuarioId(usuarioId);
  });
});

// Inicializa as tabelas de contatos e usuários
function initializeDataTables() {
  getDatatable('#table-contatos');
  getDatatable('#table-usuarios');
}

function fetchContatosPorUsuarioId(usuarioId) {
  $.ajax({
    type: 'GET',
    url: `/Usuario/ListarContatosPorUsuarioId/${usuarioId}`,
    success: function (result) {
      $("#listaContatosUsuario").html(result);
      $('#modalContatosUsuario').modal('show');
      getDatatable('#table-contatos-usuario');
    },
    error: function (xhr, status, error) {
      console.error('Erro ao buscar contatos: ', error);
    }
  });
}

function getDatatable(id) {
  const tableOptions = {
    ordering: true,
    paging: true,
    searching: true,
    language: {
      emptyTable: 'Nenhum registro encontrado na tabela.',
      info: 'Mostrar _START_ até _END_ de _TOTAL_ registros',
      infoEmpty: 'Mostrar 0 até 0 de 0 Registros',
      infoFiltered: '(Filtrar de _MAX_ total registros)',
      lengthMenu: 'Mostrar _MENU_ registros por página',
      loadingRecords: 'Carregando...',
      processing: 'Processando...',
      zeroRecords: 'Nenhum registro encontrado',
      search: 'Pesquisar',
      paginate: {
        next: 'Próximo',
        previous: 'Anterior',
        first: 'Primeiro',
        last: 'Último',
      },
      aria: {
        sortAscending: ': Ordenar colunas de forma ascendente',
        sortDescending: ': Ordenar colunas de forma descendente',
      },
    },
  };

  $(id).DataTable(tableOptions);
}

$('.close-alert').click(function () {
  $('.alert').hide('hide');
});
