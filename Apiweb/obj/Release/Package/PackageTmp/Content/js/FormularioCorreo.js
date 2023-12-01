function habilitarCorreo(TIPO,PASO_ACTUAL) {
    Correos(TIPO,PASO_ACTUAL);
    $('#ModalCorreo').modal('show');

}

function Correos(TIPO,PASO) {

    $('#DtCorreos').DataTable().destroy();
    CorreoEditor = new $.fn.dataTable.Editor({
        ajax: "/Correos/FormularioCorreo?tipo="+TIPO+"&paso=" + PASO,
        table: "#DtCorreos",
        fields: [

            /* { label: "ID:", name: "ID" },*/
            /* { label: "ID:", name: "ID", def: table.rows({ selected: true }).data()[0].ID, type: "readonly"},*/
            { label: "USUARIO:", name: "USUARIO" },
            { label: "CORREO:", name: "CORREO" },
            { label: "TIPO:", name: "TIPO", def: TIPO },
            { label: "PASO ACTUAL:", name: "PASO_ACTUAL", def: PASO },


        ]
    });

    $('#DtCorreos').on('click', 'tbody ul.dtr-details li', function (e) {
        // Edit the value, but this selector allows clicking on label as well
        CorreoEditor.inline($('span.dtr-data', this));
    });



    Tcorreos = $('#DtCorreos').DataTable({
        responsive: true,
        dom: "Bfrltip",
        //"scrollY": "500px",
        "iDisplayLength": 5,
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        ajax: "/Correos/FormularioCorreo?tipo=" + TIPO + "&paso=" + PASO,


        columns: [
            { data: "ID" },
            { data: "USUARIO" },
            { data: "CORREO" },

        ],

        order: [0, 'asc'],
        keys: {
            /*columns: ':not(:first-child)',*/
            editor: CorreoEditor
        },
        select: true,
        buttons: [

            {
                extend: "createInline",
                editor: CorreoEditor,
                formOptions: {
                    submitTrigger: 0,
                    submitHtml: '<i class="fa fa-play"/>'
                }
            }, {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel" style="color:green"></i> ',
                titleAttr: 'Exportar a Excel',
                className: 'btn btn-success btn-sm'
            },

            { extend: 'remove', className: 'btn-light', editor: CorreoEditor },

        ],
        language: {
            url: "/Content/idioma/es-ES.json"
                }
            });
        }