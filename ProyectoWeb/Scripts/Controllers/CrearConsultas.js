var tabladata;

$(document).ready(function () {
    ListarEmpleadosMaster();
    jQuery.ajax({
        url: $.MisUrls.url.UrlGetListarEmpleado,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#cboEmpleado").html("");
            if (data != null) {

                $.each(data.data, function (i, item) {
                    if (item.Activo) {
                        $("<option>").attr({ "value": item.IdEmpleado }).text(item.Nombres+" "+item.Apellidos).appendTo("#cboEmpleado");
                    }
                })
                $("#cboEmpleado").val($("#cboEmpleado option:first").val());
                
            }
        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {

        },
    });

    //validamos el formulario
    //$("#formdata").validate({
    //    rules: {
    //        arealaboral: "required",
    //        nombrecortoarea: "required",
    //        codigoarea: "required"

    //    },
    //    messages: {
    //        arealaboral: "Ingresar Area Laboral",
    //        nombrecortoarea: "Ingresar Nombre Corto de Area",
    //        codigoarea: "Ingresar el Codigo de Area"
    //    }
    //});

    $('#dtpfechaconsulta').datepicker();
//    tabladata = $('#tbdata').DataTable({
//        "ajax": {
//            "url": $.MisUrls.url.UrlGetListarEmpleado,
//            "type": "GET",
//            "datatype": "json"
//        },
//        "columns": [
//            { "data": "DNI" },
//            { "data": "Nombres" },
//            { "data": "Apellidos" },
//            //{
//            //    "data": "FechaNacimiento", render: function (data) {
//            //        return ObtenerFormatoFecha(data)
//            //    }
//            //},

//            { "data": "PuestoTrabajo" },
//            {
//                "data": "Activo", "render": function (data) {
//                    if (data) {
//                        return "Activo"
//                    } else {
//                        return "No Activo"
//                    }
//                }
//            },
//            {
//                "data": "IdEmpleado", "render": function (data, type, row, meta) {
//                    return "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
//                },
//                "orderable": false,
//                "searchable": false,
//                "width": "150px"
//            }
//        ],
//        "language": {
//            "url": $.MisUrls.url.Url_datatable_spanish
//        }
//    });

    //
});

function ListarEmpleadosMaster() {
    jQuery.ajax({
        url: $.MisUrls.url.UrlGetListarEmpleado,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#cboEmpleados").html("");
            if (data != null) {

                $.each(data.data, function (i, item) {
                    if (item.Activo) {
                        $("<option>").attr({ "value": item.IdEmpleado }).text(item.Nombres + " " + item.Apellidos).appendTo("#cboEmpleados");
                    }
                })
                $("#cboEmpleados").val($("#cboEmpleados option:first").val());

            }
        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {

        },
    });
    
}

function abrirPopUpForm(json) {

    $("#txtid").val(0);

    if (json != null) {
        $("#txtid").val(json.IdConsulta);
        $("#dtpfechaconsulta").val(ObtenerFormatoFecha(json.FechaConsulta));
        $("#txtprofesional").val(json.Profesional);
        $("#txtmotivoconsulta").val(json.MotivoConsulta); 
        $("#txtenfermedadactual").val(json.EnfermedadActual);
        $("#txtanamnesis").val(json.Anamnesis);
        $("#txtorientaciondiag").val(json.OrientacionDiagnostica);
        $("#txtcontigencia").val(json.Contigencia);
        var valor = 0;
        valor = json.Activo == true ? 1 : 0
        $("#cboEstado").val(valor);
    }
    else {
        $("#dtpfechaconsulta").val("");
        $("#txtprofesional").val("");
        $("#txtmotivoconsulta").val("");
        $("#txtenfermedadactual").val("");
        $("#txtanamnesis").val("");
        $("#txtorientaciondiag").val("");
        $("#txtcontigencia").val("");
        $("#cboEstado").val(1);
    }

    $('#FormModal').modal('show');

}
function Buscar() {

    var IdEmp = parseInt($("#cboEmpleados").val());

    //obtenemos las vacantes
    jQuery.ajax({
        url: $.MisUrls.url.UrlGetListarConsultas + "?IdEmpleado=" + IdEmp,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#tbdata tbody").html("");
            $('#FormModal').modal('hide');
            if (data != null) {

                $.each(data, function (i, row) {

                    $("<tr>").append(
                        $("<td>").text(row.IdEmpleado.Nombres),
                        $("<td>").text(row.IdEmpleado.Apellidos),
                        $("<td>").text(ObtenerFormatoFecha(row.FechaConsulta)),
                        $("<td>").text(row.Profesional),
                        $("<td>").text(row.MotivoConsulta),
                        $("<td>").text(row.EnfermedadActual),
                        $("<td>").text(row.Anamnesis),
                        $("<td>").text(row.OrientacionDiagnostica),
                        $("<td>").text(row.Contigencia),
                        $("<td>").text(row.Activo ? "Activo" : "No Activo"),
                        $("<td>").append(
                            $("<button>")
                                .addClass('btn btn-primary btn-sm')
                                .attr('type', 'button')
                                .on('click', function () {
                                    abrirPopUpForm(row);
                                })
                                .html("<i class='fas fa-pen'></i>"),
                            $("<button>")
                                .addClass('btn btn-danger btn-sm ml-2')
                                .attr('type', 'button')
                                .on('click', function () {
                                    eliminar(row.IdConsulta);
                                })
                                .html("<i class='fa fa-trash'></i>")
                        ),
                        //$("<td>").append(
                        //    $("<button>")
                        //        .addClass('btn btn-danger btn-sm ml-2')
                        //        .attr('type', 'button')
                        //        .on('click', function () {
                        //            eliminar(row.IdConsulta);
                        //        })
                        //        .html("<i class='fa fa-trash'></i>")
                        //)

                        //$("<td>").append(
                        //    $("<input>").attr({ "type": "number" }).val(row.TotalVacantes)
                        //)
                    ).data("data", row).appendTo("#tbdata tbody");

                })

            } else {
                //$('#FormModal').modal('hide');
                swal("Mensaje", "Este empleado no tiene consultas registradas", "warning")
            }

        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {

        },
    });
}

function Guardar() {


    if ($("#formdata").valid()) {

        var $data = {
            oConsulta: {
                IdConsulta: parseInt($("#txtid").val()),
                IdEmpleado: {
                        IdEmpleado: parseInt($("#cboEmpleado").val())
                },
                /*IdEmpleado: parseInt($("#cboEmpleado").val()),*/
                FechaConsulta: $("#dtpfechaconsulta").val(),
                Profesional: $("#txtprofesional").val(),
                MotivoConsulta: $("#txtmotivoconsulta").val(),
                EnfermedadActual: $("#txtenfermedadactual").val(),
                Anamnesis: $("#txtanamnesis").val(),
                OrientacionDiagnostica: $("#txtorientaciondiag").val(),
                Contigencia: $("#txtcontigencia").val(),
                Activo: parseInt($("#cboEstado").val()) == 1 ? true : false
            }
        }


        jQuery.ajax({
            url: $.MisUrls.url.UrlPostGuardarConsultas,
            type: "POST",
            data: JSON.stringify($data),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {
                    //tabladata.ajax.reload();
                    Buscar();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Registro exitoso", "success")
                } else {

                    swal("Mensaje", "No se pudo guardar los cambios", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    }

}

function eliminar($idconsulta) {
    if (confirm("¿Desea inactivar la consulta seleccionada?")) {
        jQuery.ajax({
            url: $.MisUrls.url.UrlGetEliminarConsultas + "?idconsulta=" + $idconsulta,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {
                    Buscar();
                    swal("Mensaje", "Registro Eliminado exitosamente", "success")
                } else {
                    swal("Mensaje", "No se pudo inactivar el empleado", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    }
}
function ObtenerFormatoFecha(datetime) {

    var re = /-?\d+/;
    var m = re.exec(datetime);
    var d = new Date(parseInt(m[0]))


    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '-' + (('' + month).length < 2 ? '0' : '') + month + '-' + d.getFullYear();

    return output;
}

