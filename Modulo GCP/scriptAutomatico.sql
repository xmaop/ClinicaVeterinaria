--///////////////////////////////////////////////////////////////////////////////////////////////////
-- QUERY QUE ANULA LAS ORDENES DE ATENCION NO ATENDIDAS HASTA LA FECHA ACTUAL
--///////////////////////////////////////////////////////////////////////////////////////////////////
update GCP_OrdenAtencion
set estado = 'AN'
where fecha < convert(date, getdate())
and estado = 'GE';

--///////////////////////////////////////////////////////////////////////////////////////////////////
-- QUERY QUE SIMULA EL ENVIO DE LAS NOTIFICACIONES AUTOMATICAS VIA EMAIL PASADAS A LA FECHA ACTUAL
--///////////////////////////////////////////////////////////////////////////////////////////////////
-- PASO 1:
with cte as
(
select
	ord.idordenAtencion,
	(select replace(replace(descripcion,'{0}', ord.codigo), '{1}', convert(varchar, ord.fecha,103)) from GCP_Parametro where id_Parametro = 1) asunto,
	(select replace(replace(replace(replace(descripcion, '{0}', case when cli.Razon_Social is null then cli.Nom_Cliente + ' ' + cli.ApePat_Cliente + ' ' + cli.ApeMat_Cliente else cli.Razon_Social end), '{1}', ord.codigo), '{2}', convert(varchar, ord.fecha,103)), '{3}', tur.horaInicio)
	 from GCP_Parametro where id_Parametro = 2) detalle
from GCP_Paciente pac
inner join GCP_Cliente cli
on pac.Id_Cliente = cli.idCliente
inner join GCP_OrdenAtencion ord
on ord.id_Mascota = pac.Id_Mascota
inner join GCP_Turno tur
on ord.id_Turno = tur.id_Turno
where fecha < convert(date, getdate())
and flgNotificar = 'N'
)
insert into GCP_Notificacion
select asunto, detalle, idordenAtencion, getdate()
from cte;

-- PASO 2:
update GCP_OrdenAtencion
set flgNotificar = 'E'
where idOrdenAtencion in (
	select idOrdenAtencion
	from GCP_OrdenAtencion
	where fecha < convert(date, getdate())
) and flgNotificar = 'N';