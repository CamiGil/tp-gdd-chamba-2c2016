USE [GD2C2016]
GO
/****** Object:  Schema [CHAMBA]    Script Date: 2/11/2016 1:28:57 a. m. ******/
CREATE SCHEMA [CHAMBA]
GO
/****** Object:  StoredProcedure [CHAMBA].[AfiliadosBonos]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[AfiliadosBonos] (@Semestre int, @Mes int, @Año int)
AS
BEGIN


		DECLARE @parametros varchar(5)

		IF (@Mes = 0)
			IF (@Semestre = 1)
				SET @Parametros = '< 6'
			ELSE
				SET @Parametros = '> 6'
		ELSE
			SET @Parametros = '= ' + CAST(@Mes AS varchar(2))


		EXEC('SELECT TOP 5 Usua_Nombre + '' '' + Usua_Apellido AS ''Afiliado'', COUNT(*) AS ''Cantidad'', 
		
			(SELECT CASE COUNT(*) WHEN 1 THEN ''No'' ELSE ''Si'' END 
			FROM CHAMBA.Pacientes P2 
			WHERE CAST(SUBSTRING(CAST(P2.Paci_Numero AS varchar(18)), 1, LEN(CAST(P2.Paci_Numero AS varchar(18))) - 2) AS numeric(16, 0)) = CAST(SUBSTRING(CAST(P1.Paci_Numero AS varchar(18)), 1, LEN(CAST(P1.Paci_Numero AS varchar(18))) - 2) AS numeric(16, 0))
			) AS ''Grupo familiar''

		FROM CHAMBA.Compra_Bonos
			JOIN CHAMBA.Bonos ON Comp_Bono_Id = Bono_Compra
			JOIN CHAMBA.Usuarios ON Comp_Bono_Paciente = Usua_Id
			JOIN CHAMBA.Pacientes P1 ON P1.Paci_Usuario = Usua_Id
		WHERE YEAR(Comp_Bono_Fecha) = '+@Año+'
			AND MONTH(Comp_Bono_Fecha) '+@Parametros+'

		GROUP BY Usua_Nombre + '' '' + Usua_Apellido, P1.Paci_Numero
		ORDER BY COUNT(*) DESC')				
END






GO
/****** Object:  StoredProcedure [CHAMBA].[AGREGAR_DISPONIBILIDAD_EN_AGENDA]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[AGREGAR_DISPONIBILIDAD_EN_AGENDA]
				(
					@Profesional numeric(18,0),
					@Especialidad numeric(18,0),
					@Fecha datetime
				)
AS
BEGIN


	/* CREACION DE NUEVO ELEMENTO EN LA AGENDA */
	INSERT INTO CHAMBA.Agenda(Agen_Profesional,Agen_Especialidad,Agen_Fecha, Agen_Ocupado) VALUES (@Profesional,@Especialidad,@Fecha, 0)
END

GO
/****** Object:  StoredProcedure [CHAMBA].[AsignarFuncionalidad]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[AsignarFuncionalidad] (@idRol numeric(18,0), @idFunc numeric(18,0))
as
begin
insert into CHAMBA.Funcionalidad_X_Rol (Func_X_Rol_Rol, Func_X_Rol_Funcionalidad)
values(@idRol, @idFunc)
end





GO
/****** Object:  StoredProcedure [CHAMBA].[CargarAfiliados]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[CargarAfiliados] (@Afiliado varchar(18), @Nombre varchar(255), @Apellido varchar(255), @Documento varchar(18))
AS
BEGIN
SELECT Paci_Numero AS 'Nro. Afiliado', Usua_Nombre AS 'Nombre', Usua_Apellido AS 'Apellido', Usua_DNI AS 'Documento', Usua_Telefono AS 'Telefono', Usua_Mail AS 'Email'
FROM CHAMBA.Pacientes JOIN CHAMBA.Usuarios ON Paci_Usuario = Usua_Id
WHERE CONVERT(varchar(18), Paci_Numero) LIKE '%'+ISNULL(@Afiliado, '')+'%' 
AND Usua_Nombre LIKE '%'+ISNULL(@Nombre, '')+'%' 
AND Usua_Apellido LIKE '%'+ISNULL(@Apellido, '')+'%' 
AND CONVERT(varchar(18), Usua_DNI) LIKE '%'+ISNULL(@Documento, '')+'%'
AND Paci_Fecha_Baja IS NULL ORDER BY Paci_Numero
RETURN
END









GO
/****** Object:  StoredProcedure [CHAMBA].[CargarBonosParaTurno]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[CargarBonosParaTurno] (@Turno numeric(18,0))
AS
BEGIN

SELECT Bono_Numero FROM CHAMBA.Bonos 
JOIN CHAMBA.Compra_Bonos ON Bono_Compra = Comp_Bono_Id
JOIN CHAMBA.Turnos ON Turn_Numero = @Turno
JOIN CHAMBA.Pacientes P1 ON Turn_Paciente = P1.Paci_Usuario
JOIN CHAMBA.Pacientes P2 ON Comp_Bono_Paciente= P2.Paci_Usuario

WHERE Bono_Turno_Uso IS NULL
AND P1.Paci_Plan = Comp_Bono_Plan
AND CAST(SUBSTRING(CAST(P2.Paci_Numero AS varchar(18)), 1, LEN(CAST(P2.Paci_Numero AS varchar(18))) - 2) AS numeric(16, 0)) = CAST(SUBSTRING(CAST(P1.Paci_Numero AS varchar(18)), 1, LEN(CAST(P1.Paci_Numero AS varchar(18))) - 2) AS numeric(16, 0))

END




GO
/****** Object:  StoredProcedure [CHAMBA].[CargarFuncionalidades]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[CargarFuncionalidades]
AS 
BEGIN
SELECT Func_Descripcion FROM CHAMBA.Funcionalidades
END



GO
/****** Object:  StoredProcedure [CHAMBA].[CargarRoles]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[CargarRoles] 
as
begin
select Rol_Nombre from CHAMBA.Roles
end



GO
/****** Object:  StoredProcedure [CHAMBA].[CargarRolesHabilitados]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[CargarRolesHabilitados] 
as
begin
select Rol_Nombre from CHAMBA.Roles where Rol_Estado = 1
end




GO
/****** Object:  StoredProcedure [CHAMBA].[CargarTurnos]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[CargarTurnos] (@Profesional numeric(18,0), @Especialidad numeric(18,0), @Fecha datetime)
AS
BEGIN

IF (@Profesional = 0)
	BEGIN

	SELECT Turn_Numero AS 'Numero', (CAST(FORMAT(DATEPART(HOUR, Agen_Fecha), '0#') AS varchar) + ':' + CAST(FORMAT(DATEPART(MINUTE, Agen_Fecha), '0#') AS varchar)) AS 'Hora', (U1.Usua_Nombre + ' ' + U1.Usua_Apellido) AS 'Paciente', (U2.Usua_Nombre + ' ' + U2.Usua_Apellido) AS 'Profesional'
	FROM CHAMBA.Turnos 
	JOIN CHAMBA.Usuarios AS U1 ON Turn_Paciente = U1.Usua_Id
	JOIN CHAMBA.Agenda ON Turn_Agenda = Agen_Id
	JOIN CHAMBA.Profesionales ON Agen_Profesional = Prof_Usuario
	JOIN CHAMBA.Usuarios AS U2 ON Prof_Usuario = U2.Usua_Id
	LEFT JOIN CHAMBA.Consultas ON Cons_Turno = Turn_Numero
	WHERE DAY(Agen_Fecha) = DAY(@Fecha) AND MONTH(Agen_Fecha) = MONTH(@Fecha) AND YEAR(Agen_Fecha) = YEAR(@Fecha) 
	AND Turn_Cancelado IS NULL
	AND Agen_Cancelado IS NULL
	AND Agen_Especialidad = @Especialidad
	AND Turn_Fecha_Llegada IS NULL
	AND Cons_Sintoma IS NULL AND Cons_Diagnostico IS NULL
	ORDER BY Agen_Fecha
	END

ELSE

	BEGIN 

	SELECT Turn_Numero AS 'Numero', (CAST(FORMAT(DATEPART(HOUR, Agen_Fecha), '0#') AS varchar) + ':' + CAST(FORMAT(DATEPART(MINUTE, Agen_Fecha), '0#') AS varchar)) AS 'Hora', (U1.Usua_Nombre + ' ' + U1.Usua_Apellido) AS 'Paciente', (U2.Usua_Nombre + ' ' + U2.Usua_Apellido) AS 'Profesional'
	FROM CHAMBA.Turnos 
	JOIN CHAMBA.Usuarios AS U1 ON Turn_Paciente = U1.Usua_Id
	JOIN CHAMBA.Agenda ON Turn_Agenda = Agen_Id
	JOIN CHAMBA.Profesionales ON Agen_Profesional = Prof_Usuario
	JOIN CHAMBA.Usuarios AS U2 ON Prof_Usuario = U2.Usua_Id
	LEFT JOIN CHAMBA.Consultas ON Cons_Turno = Turn_Numero
	WHERE DAY(Agen_Fecha) = DAY(@Fecha) AND MONTH(Agen_Fecha) = MONTH(@Fecha) AND YEAR(Agen_Fecha) = YEAR(@Fecha) 
	AND Turn_Cancelado IS NULL
	AND Agen_Cancelado IS NULL
	AND Agen_Especialidad = @Especialidad
	AND Turn_Fecha_Llegada IS NULL
	AND Agen_Profesional = @Profesional
	AND Cons_Sintoma IS NULL AND Cons_Diagnostico IS NULL
	ORDER BY Agen_Fecha
	END

END






GO
/****** Object:  StoredProcedure [CHAMBA].[ObtenerNuevoIdBono]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ObtenerNuevoIdBono] (@id numeric(18, 0) OUT)
AS BEGIN
	DECLARE @Existe INT = 1

	SELECT @id = MAX(Bono_Numero) FROM CHAMBA.Bonos

	WHILE(@Existe <> 0)
		BEGIN
			SET @id = @id + 1
			SELECT @Existe = COUNT(*) FROM CHAMBA.Bonos WHERE Bono_Numero = @id
		END
END






GO
/****** Object:  StoredProcedure [CHAMBA].[ComprarBonos]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ComprarBonos] (@Afiliado varchar(18), @Cantidad INT, @Fecha datetime)
AS
BEGIN
	DECLARE @Usuario numeric(18,0), @Plan numeric(18,0), @Compra numeric(18,0), @Valor numeric(18,0), @i INT = 0, @Numero numeric(18,0)
	SELECT @Usuario = Paci_Usuario, @Plan = Paci_Plan FROM CHAMBA.Pacientes WHERE Paci_Numero = @Afiliado

	INSERT INTO Compra_Bonos (Comp_Bono_Fecha, Comp_Bono_Paciente, Comp_Bono_Plan) VALUES (@Fecha, @Usuario, @Plan)

	SET @Compra = @@IDENTITY

	SELECT @Valor = Plan_Precio_Bono_Consulta FROM Planes WHERE Plan_Codigo = @Plan

	WHILE (@i < @Cantidad)
		BEGIN

		EXEC CHAMBA.ObtenerNuevoIdBono @Numero OUT

		INSERT INTO Bonos (Bono_Numero, Bono_Compra, Bono_Valor) VALUES (@Numero, @Compra, @Valor)

		SET @i = @i + 1
		
		END
END




GO
/****** Object:  StoredProcedure [CHAMBA].[CrearRolNuevo]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[CrearRolNuevo] (@nombre varchar(255))
as
begin
insert into CHAMBA.Roles(Rol_Nombre, Rol_Estado)
values(@nombre,1)
end







GO
/****** Object:  StoredProcedure [CHAMBA].[DIAS_DISPONIBLES_PROFESIONAL_POR_ESPECIALIDAD]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[DIAS_DISPONIBLES_PROFESIONAL_POR_ESPECIALIDAD] 
				(	@Profesional numeric(18,0),
					@Especialidad numeric(18,0),
					@FechaInicio datetime
				)
AS 
BEGIN
	SELECT Agen_Fecha FROM CHAMBA.Agenda WHERE 
	Agen_Ocupado <> 1 
	AND Agen_Fecha >= @FechaInicio
	AND Agen_Profesional = @Profesional
	AND Agen_Especialidad = @Especialidad
	And Agen_Cancelado IS NULL
END

GO
/****** Object:  StoredProcedure [CHAMBA].[EliminarAfiliado]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[EliminarAfiliado](@Afiliado numeric(18, 0), @Fecha datetime)

AS
BEGIN

UPDATE CHAMBA.Pacientes SET Paci_Fecha_Baja = @Fecha WHERE Paci_Numero = @Afiliado

INSERT INTO CHAMBA.Cancelaciones(Canc_Descripcion,Canc_Tipo) VALUES('Baja de paciente',1) 
UPDATE CHAMBA.Turnos SET Turn_Cancelado=@@identity WHERE Turn_Paciente = @Afiliado  
UPDATE CHAMBA.Agenda SET Agen_Ocupado = 0 WHERE Agen_Id = (SELECT Turn_Agenda FROM CHAMBA.Turnos WHERE Turn_Paciente = @Afiliado)

END




GO
/****** Object:  StoredProcedure [CHAMBA].[EliminarFuncionalidades]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[EliminarFuncionalidades] (@rol numeric(18,0))
as
begin
delete from CHAMBA.Funcionalidad_X_Rol
where Func_X_Rol_Rol = @rol
end






GO
/****** Object:  StoredProcedure [CHAMBA].[EsAfiliado]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[EsAfiliado] (@Usuario numeric(18, 0), @Rol numeric(18,0), @Resultado INT OUT)
AS
BEGIN
	DECLARE @Nombre varchar(255)
	SELECT @Nombre = Rol_Nombre FROM CHAMBA.Rol_X_Usuario JOIN CHAMBA.Roles ON Rol_X_Usua_Rol = Rol_Id WHERE Rol_X_Usua_Usuario = @Usuario AND Rol_X_Usua_Rol = @Rol

	IF (@Nombre = 'Afiliado')
		SET @Resultado = 1
	ELSE
		SET @Resultado = 0
END






GO
/****** Object:  StoredProcedure [CHAMBA].[EspecialidadesBonos]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[EspecialidadesBonos] (@Semestre int, @Mes int, @Año int)
AS
BEGIN

		DECLARE @parametros varchar(5)

		IF (@Mes = 0)
			IF (@Semestre = 1)
				SET @Parametros = '< 6'
			ELSE
				SET @Parametros = '> 6'
		ELSE
			SET @Parametros = '= ' + CAST(@Mes AS varchar(2))

		EXEC('SELECT TOP 5 Espe_Descripcion AS ''Especialidad'', COUNT(*) AS ''Cantidad''

		FROM CHAMBA.Agenda
			JOIN CHAMBA.Turnos ON Turn_Agenda = Agen_Id
			JOIN CHAMBA.Bonos ON Bono_Turno_Uso = Turn_Numero
			JOIN CHAMBA.Especialidades ON Agen_Especialidad = Espe_Codigo
		WHERE YEAR(Agen_Fecha) = '+@Año+'
			AND MONTH(Agen_Fecha) '+@Parametros+'

		GROUP BY Espe_Descripcion
		ORDER BY COUNT(*) DESC')
END







GO
/****** Object:  StoredProcedure [CHAMBA].[EspecialidadesCanceladas]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[EspecialidadesCanceladas] (@De varchar(255), @Semestre int, @Mes int, @Año int)
AS
BEGIN

	DECLARE @parametros varchar(5)

	IF (@Mes = 0)
		IF (@Semestre = 1)
			SET @Parametros = '< 6'
		ELSE
			SET @Parametros = '> 6'
	ELSE
		SET @Parametros = '= ' + CAST(@Mes AS varchar(2))

IF (@De = 'Afiliado') 

		EXEC('SELECT TOP 5 Espe_Descripcion AS ''Especialidad'', COUNT(*) AS ''Cantidad'' 

		FROM CHAMBA.Turnos 
			JOIN CHAMBA.Agenda ON Agen_Id = Turn_Agenda
			JOIN CHAMBA.Pacientes ON Turn_Paciente = Paci_Usuario 
			JOIN CHAMBA.Especialidades ON Agen_Especialidad = Espe_Codigo
		WHERE Turn_Cancelado IS NOT NULL 
			AND YEAR(Agen_Fecha) = '+@Año+'
			AND MONTH(Agen_Fecha) '+@Parametros+'
		GROUP BY Espe_Descripcion 
		ORDER BY COUNT(*) DESC')

ELSE IF (@De = 'Profesionales')

		EXEC('SELECT TOP 5 Espe_Descripcion AS ''Especialidad'', COUNT(*) AS ''Cantidad'' 

		FROM CHAMBA.Agenda
			JOIN CHAMBA.Profesionales ON Agen_Profesional = Prof_Usuario
			JOIN CHAMBA.Especialidades ON Agen_Especialidad = Espe_Codigo
		WHERE Agen_Cancelado IS NOT NULL 
			AND YEAR(Agen_Fecha) = '+@Año+'
			AND MONTH(Agen_Fecha) '+@Parametros+'
		GROUP BY Espe_Descripcion 
		ORDER BY COUNT(*) DESC')

ELSE
	EXEC('SELECT TOP 5 * FROM
		
		((SELECT TOP 5 Espe_Descripcion AS ''Especialidad'', COUNT(*) AS ''Cantidad''

		FROM CHAMBA.Turnos 
			JOIN CHAMBA.Agenda ON Agen_Id = Turn_Agenda
			JOIN CHAMBA.Pacientes ON Turn_Paciente = Paci_Usuario 
			JOIN CHAMBA.Especialidades ON Agen_Especialidad = Espe_Codigo
		WHERE Turn_Cancelado IS NOT NULL 
			AND YEAR(Agen_Fecha) = '+@Año+'
			AND MONTH(Agen_Fecha) '+@Parametros+'
		GROUP BY Espe_Descripcion ORDER BY COUNT(*) DESC) 
		
		
		UNION
		
		(SELECT TOP 5 Espe_Descripcion AS ''Especialidad'', COUNT(*) AS ''Cantidad''

		FROM CHAMBA.Agenda
			JOIN CHAMBA.Profesionales ON Agen_Profesional = Prof_Usuario
			JOIN CHAMBA.Especialidades ON Agen_Especialidad = Espe_Codigo
		WHERE Agen_Cancelado IS NOT NULL 
			AND YEAR(Agen_Fecha) = '+@Año+'
			AND MONTH(Agen_Fecha) '+@Parametros+'
		GROUP BY Espe_Descripcion 
		ORDER BY COUNT(*) DESC))
		As a
		ORDER BY ''Cantidad'' DESC')
		
END


GO
/****** Object:  StoredProcedure [CHAMBA].[ExisteRol]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[ExisteRol] (@nombre varchar(255))
as
begin
declare @existe int
set @existe = (select count(Rol_Id) from CHAMBA.Roles where Rol_Nombre = @nombre) 
return @existe
end




GO
/****** Object:  StoredProcedure [CHAMBA].[FuncionalidadesPorRol]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[FuncionalidadesPorRol] (@Rol varchar(255))
as
begin
select Func_Descripcion from CHAMBA.Funcionalidad_X_Rol join CHAMBA.Roles ON Func_X_Rol_Rol = Rol_Id join CHAMBA.Funcionalidades on
Func_Id = Func_X_Rol_Funcionalidad
where Rol_Nombre = @Rol
end




GO
/****** Object:  StoredProcedure [CHAMBA].[HabilitarRol]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[HabilitarRol] (@nombre nvarchar(30))
as
begin
update CHAMBA.Roles
set Rol_ESTADO = 1
where Rol_nombre = @nombre
end






GO
/****** Object:  StoredProcedure [CHAMBA].[HistorialCambiosPlan]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[HistorialCambiosPlan] (@Afiliado numeric(18, 0))
AS
BEGIN

DECLARE @IdUsuario numeric(18,0)

SELECT @IdUsuario = Paci_Usuario FROM CHAMBA.Pacientes WHERE Paci_Numero = @Afiliado

SELECT Camb_Plan_Fecha AS 'Fecha', (SELECT Plan_Descripcion FROM CHAMBA.Planes WHERE Plan_Codigo = Camb_Plan_Plan_Anterior) AS 'Plan anterior', (SELECT Plan_Descripcion FROM CHAMBA.Planes WHERE Plan_Codigo = Camb_Plan_Plan_Nuevo) AS 'Plan nuevo', Camb_Plan_Razon AS 'Razon' FROM CHAMBA.Cambio_Plan WHERE Camb_Plan_Paciente = @IdUsuario
RETURN 
END





GO
/****** Object:  StoredProcedure [CHAMBA].[HORARIOS_DISPONIBLES_EN_AGENDA_PROFESIONAL]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[HORARIOS_DISPONIBLES_EN_AGENDA_PROFESIONAL] 
				(	@Profesional numeric(18,0),
					@Especialidad numeric(18,0),
					@Dia INT,
					@Numero_mes INT,
					@Anio INT	
				)
AS
BEGIN
	SELECT	FORMAT(DATEPART(HOUR, Agen_Fecha), '0#') + ':' + FORMAT(DATEPART(MINUTE, Agen_Fecha), '0#') as 'Horario',
			a.Agen_Id as 'Codigo en agenda'
		FROM	 CHAMBA.Agenda a
			join CHAMBA.Profesionales p on a.Agen_Profesional = p.Prof_Usuario 
			join CHAMBA.Usuarios u on u.Usua_Id = p.Prof_Usuario
			join CHAMBA.Especialidades e on e.Espe_Codigo = a.Agen_Especialidad
		WHERE p.Prof_Usuario = @Profesional
			and e.Espe_Codigo = @Especialidad
			and YEAR(a.Agen_Fecha) = @Anio
			and MONTH(a.Agen_Fecha) = @Numero_Mes
			and DAY(a.Agen_Fecha) = @Dia
			and a.Agen_Ocupado <> 1
			And a.Agen_Cancelado IS NULL
		ORDER BY a.Agen_Fecha asc
END

GO
/****** Object:  StoredProcedure [CHAMBA].[InhabilitarRol]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[InhabilitarRol] (@id numeric(18,0))
as
begin
update CHAMBA.Roles
set Rol_Estado = 0
where Rol_Id = @id
end



GO
/****** Object:  StoredProcedure [CHAMBA].[InhabilitarRolPorUsuario]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[InhabilitarRolPorUsuario] (@id numeric(18,0))
as
begin
delete from CHAMBA.Rol_X_Usuario
where Rol_X_Usua_Usuario = @id
end





GO
/****** Object:  StoredProcedure [CHAMBA].[ModificarAfiliado]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ModificarAfiliado](@Afiliado numeric(18, 0), @Nombre varchar(255), 
@Apellido varchar(255), @TipoDocumento numeric(3,0), @Documento numeric(18,0), 
@Domicilio varchar(255), @Telefono numeric(18,0), @Email varchar(255), @FechaNac datetime, 
@Sexo char(1), @EstadoCivil numeric(2,0), @CantHijos numeric(3,0), @Plan numeric(18,0))

AS
BEGIN

DECLARE @Existe INT

SELECT @Existe = COUNT(*) FROM CHAMBA.Pacientes WHERE Paci_Numero = @Afiliado

IF (@Existe = 0) 
BEGIN
	INSERT INTO CHAMBA.Usuarios (Usua_Nombre, Usua_Apellido, Usua_TipoDNI, Usua_DNI, Usua_Direccion, 
	Usua_Telefono, Usua_Mail, Usua_Fecha_Nac, Usua_Sexo, Usua_Usuario, Usua_Clave, Usua_Intentos) VALUES (@Nombre, @Apellido, @TipoDocumento, 
	@Documento, @Domicilio, @Telefono, @Email, @FechaNac, @Sexo, @Email, HASHBYTES('SHA2_256', CAST(@Documento AS VARCHAR(18))), 0)

	INSERT INTO CHAMBA.Pacientes (Paci_Usuario, Paci_Numero, Paci_Estado_Civil, Paci_Cant_Hijos, Paci_Plan) VALUES (@@IDENTITY, @Afiliado, @EstadoCivil, @CantHijos, @Plan)
END
ELSE
BEGIN
	DECLARE @IdUsuario numeric(18,0)
	SELECT @IdUsuario = Paci_Usuario FROM CHAMBA.Pacientes WHERE Paci_Numero = @Afiliado

	UPDATE CHAMBA.Usuarios SET Usua_Direccion = @Domicilio, Usua_Telefono = @Telefono, Usua_Mail = @Email,
	Usua_Fecha_Nac = @FechaNac, Usua_Sexo = @Sexo, Usua_Usuario = @Email WHERE Usua_Id = @IdUsuario

	UPDATE CHAMBA.Pacientes SET Paci_Estado_Civil = @EstadoCivil, Paci_Cant_Hijos = @CantHijos, Paci_Plan = @Plan WHERE Paci_Numero = @Afiliado
END




END








GO
/****** Object:  StoredProcedure [CHAMBA].[ModificarNombreRol]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[ModificarNombreRol] (@nombre varchar(255), @anterior varchar(255))
as
begin
update CHAMBA.Roles set Rol_Nombre = @nombre
where Rol_Nombre = @anterior
end



GO
/****** Object:  StoredProcedure [CHAMBA].[ModificarPlan]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ModificarPlan](@Afiliado numeric(18, 0), @NuevoPlan numeric(18,0), @Fecha datetime, @Razon varchar(255))
AS
BEGIN

DECLARE @IdUsuario numeric(18,0), @PlanAnterior numeric(18,0)
SELECT @IdUsuario = Paci_Usuario, @PlanAnterior = Paci_Plan FROM CHAMBA.Pacientes WHERE Paci_Numero = @Afiliado

IF (@PlanAnterior <> @NuevoPlan)
	INSERT INTO CHAMBA.Cambio_Plan (Camb_Plan_Paciente, Camb_Plan_Plan_Anterior, Camb_Plan_Plan_Nuevo, Camb_Plan_Fecha, Camb_Plan_Razon) VALUES (@IdUsuario, @PlanAnterior, @NuevoPlan, @Fecha, @Razon)

END



GO
/****** Object:  StoredProcedure [CHAMBA].[OBTENER_NUEVO_ID_TURNO]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[OBTENER_NUEVO_ID_TURNO] (@id numeric(18, 0) OUT)
AS 
BEGIN
	DECLARE @Existe INT = 1

	SELECT @id = MAX(Turn_Numero) FROM CHAMBA.Turnos

	WHILE(@Existe <> 0)
		BEGIN
			SET @id = @id + 1
			SELECT @Existe = COUNT(*) FROM CHAMBA.Turnos t WHERE t.Turn_Numero = @id
		END
END

GO
/****** Object:  StoredProcedure [CHAMBA].[ObtenerFuncionalidadId]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[ObtenerFuncionalidadId] (@descripcion varchar(255))
as
begin
declare @id numeric(18,0)
set @id = (select Func_Id from CHAMBA.Funcionalidades where Func_Descripcion = @descripcion)
return @id
end





GO
/****** Object:  StoredProcedure [CHAMBA].[ObtenerNuevoIdPaciente]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ObtenerNuevoIdPaciente] (@id numeric(18, 0) OUT)
AS BEGIN
	DECLARE @Existe INT = 1

	DECLARE @ultimo numeric(18, 0), @cadena varchar(18)

	SELECT @ultimo = MAX(Paci_Numero) FROM CHAMBA.Pacientes
	SET @cadena = CAST(@ultimo AS varchar(18))

	SET @ultimo = CAST(SUBSTRING(@cadena, 1, LEN(@cadena) - 2) AS numeric(16, 0))


	WHILE(@Existe <> 0)
		BEGIN
			SET @ultimo = @ultimo + 1
			SET @id = CAST(CAST(@ultimo AS varchar(16)) + '01' AS numeric(18, 0))
			SELECT @Existe = COUNT(*) FROM CHAMBA.Pacientes WHERE Paci_Numero = @id
		END

END







GO
/****** Object:  StoredProcedure [CHAMBA].[ObtenerPrecioDeBono]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ObtenerPrecioDeBono] (@Afiliado numeric(18, 0), @AfiliadoNombre varchar(510) OUT, @Cantidad INT, @Unitario NUMERIC(18,0) OUT, @Total NUMERIC(18,0) OUT)
AS
BEGIN

	SELECT @Unitario = Plan_Precio_Bono_Consulta, @AfiliadoNombre = Usua_Nombre + ' ' + Usua_Apellido FROM CHAMBA.Planes JOIN CHAMBA.Pacientes ON Plan_Codigo = Paci_Plan JOIN CHAMBA.Usuarios ON Paci_Usuario = Usua_Id WHERE Paci_Numero = @Afiliado

	SET @Total = @Cantidad * @Unitario

END






GO
/****** Object:  StoredProcedure [CHAMBA].[ObtenerProximoIdFamiliar]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ObtenerProximoIdFamiliar](@Afiliado numeric(16, 0), @id int OUT)
AS
BEGIN
	DECLARE @Existe INT = 1
	SET @id = 2 /* Tiene que empezar a buscar en 3 pero como despues hago un incremento empieza en 2 */

	WHILE(@Existe <> 0)
	BEGIN
		SET @id = @id + 1
		SELECT @Existe = COUNT(*) FROM CHAMBA.Pacientes WHERE Paci_Numero = CAST(CAST(@Afiliado as varchar(16)) + FORMAT(@id,'0#') AS numeric(18, 0))	
	END
END






GO
/****** Object:  StoredProcedure [CHAMBA].[ObtenerRolId]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [CHAMBA].[ObtenerRolId] (@nombre varchar(255))
as
begin
declare @id numeric(18,0)
set @id = (select Rol_Id from CHAMBA.Roles where Rol_Nombre = @nombre)
return @id
end



GO
/****** Object:  StoredProcedure [CHAMBA].[PacienteCancelaTurno]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [CHAMBA].[PacienteCancelaTurno] (@Turno numeric(18,0), @Motivo varchar(255), @Tipo numeric(1,0))
AS
BEGIN
	INSERT INTO CHAMBA.Cancelaciones(Canc_Descripcion,Canc_Tipo) VALUES(@Motivo,@Tipo) 
	UPDATE CHAMBA.Turnos SET Turn_Cancelado=@@identity WHERE Turn_Numero = @Turno  
	UPDATE CHAMBA.Agenda SET Agen_Ocupado = 0 WHERE Agen_Id = (SELECT Turn_Agenda FROM CHAMBA.Turnos WHERE Turn_Numero = @Turno)
END

GO
/****** Object:  StoredProcedure [CHAMBA].[PosiblesPacientes]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
             


/****REGISTRAR ATENCIÓN MÉDICA***/

CREATE PROCEDURE [CHAMBA].[PosiblesPacientes](@IdMedico numeric (18,0), @fecha DateTime)
AS
BEGIN
	SELECT T.Turn_Numero, U.Usua_Nombre + ' ' + U.Usua_Apellido nombreUsuario 
	FROM CHAMBA.Turnos T 
	JOIN CHAMBA.Pacientes P ON (T.Turn_Paciente = P.Paci_Usuario)
	JOIN CHAMBA.Usuarios U ON(U.Usua_Id = P.Paci_Usuario)
	JOIN CHAMBA.Agenda A ON (A.Agen_Id = T.Turn_Agenda)
	LEFT JOIN CHAMBA.Consultas ON Cons_Turno = T.Turn_Numero
	WHERE @IdMedico = A.Agen_Profesional
	AND DAY(@fecha) = DAY(Turn_Fecha_Llegada)
	AND MONTH(@fecha) = MONTH(Turn_Fecha_Llegada)
	AND YEAR(@fecha) = YEAR(Turn_Fecha_Llegada)
	AND Cons_Sintoma IS NULL AND Cons_Diagnostico IS NULL
END 

GO
/****** Object:  StoredProcedure [CHAMBA].[ProfesionalCancelaTurno]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [CHAMBA].[ProfesionalCancelaTurno]( @FechaInicial dateTime, @FechaFinal dateTime,@Tipo numeric(1,0), @Motivo varchar(255), @Profesional numeric(18,0))
AS
BEGIN
	INSERT INTO CHAMBA.Cancelaciones(Canc_Descripcion,Canc_Tipo) VALUES(@Motivo, @Tipo)
	UPDATE CHAMBA.Agenda  SET Agen_Cancelado=@@identity WHERE Agen_Id IN(SELECT Agen_Id																		
																		FROM CHAMBA.Agenda
																		JOIN CHAMBA.Profesionales P ON (P.Prof_Usuario= Agen_Profesional)
																		WHERE P.Prof_Usuario = @Profesional
																		AND Agen_Fecha >= @FechaInicial
																		AND Agen_Fecha <= @FechaFinal)
END

GO
/****** Object:  StoredProcedure [CHAMBA].[PROFESIONALES_POR_ESPECIALIDAD]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[PROFESIONALES_POR_ESPECIALIDAD] (@Especialidad numeric(18,0), @Nombre_profesional varchar(255), @Apellido_profesional varchar(255))
AS
BEGIN
	SELECT  p.Prof_Usuario as 'Codigo del profesional',
			u.Usua_Nombre as 'Nombre',
			u.Usua_Apellido as 'Apellido'
		FROM	 CHAMBA.Profesionales p
			join CHAMBA.Usuarios u on p.Prof_Usuario = u.Usua_Id
			join CHAMBA.Tipo_Especialidad_X_Profesional tep on tep.Tipo_Espe_X_Prof_Profesional = p.Prof_Usuario 
			join CHAMBA.Tipo_Especialidad te on te.Tipo_Espe_Codigo = tep.Tipo_Espe_X_Prof_Tipo_Especialidad
			join CHAMBA.Especialidades e on e.Espe_Codigo = te.Tipo_Espe_Especialidad
		WHERE e.Espe_Codigo = @Especialidad AND u.Usua_Nombre LIKE '%'+@Nombre_profesional+'%' AND u.Usua_Apellido LIKE '%'+@Apellido_profesional+'%'
END

GO
/****** Object:  StoredProcedure [CHAMBA].[ProfesionalesConsultados]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ProfesionalesConsultados] (@Plan numeric(18,0), @Especialidad numeric(18,0), @Semestre int, @Mes int, @Año int)
AS
BEGIN

		DECLARE @parametros varchar(5)
		DECLARE @parametrosPlan varchar(20)
		DECLARE @parametrosEspecialidad varchar(20)

		IF (@Mes = 0)
			IF (@Semestre = 1)
				SET @Parametros = '< 6'
			ELSE
				SET @Parametros = '> 6'
		ELSE
			SET @Parametros = '= ' + CAST(@Mes AS varchar(2))

		IF (@Plan = 0)
			SET @ParametrosPlan = ''
		ELSE
			SET @ParametrosPlan = ' AND Paci_Plan = ' + CAST(@Plan AS varchar(18))
	
		IF (@Especialidad = 0)
			SET @ParametrosEspecialidad = ''
		ELSE
			SET @ParametrosEspecialidad = ' AND Espe_Codigo = ' + CAST(@Especialidad AS varchar(18))


		EXEC('SELECT TOP 5 Usua_Nombre + '' '' + Usua_Apellido AS ''Profesional'', Plan_Descripcion as ''Plan'',Espe_Descripcion AS ''Especialidad'', COUNT(*) AS ''Cantidad'' 

		FROM CHAMBA.Agenda
			JOIN CHAMBA.Turnos ON Agen_Id = Turn_Agenda
			JOIN CHAMBA.Consultas ON Cons_Turno = Turn_Numero
			JOIN CHAMBA.Usuarios ON Agen_Profesional = Usua_Id
			JOIN CHAMBA.Pacientes ON Turn_Paciente = Paci_Usuario
			JOIN CHAMBA.Especialidades ON Agen_Especialidad = Espe_Codigo
			JOIN CHAMBA.Planes ON Plan_Codigo = Paci_Plan
		WHERE 
			YEAR(Agen_Fecha) = '+@Año+'
			AND MONTH(Agen_Fecha) '+@Parametros+'
			'+@ParametrosPlan+@ParametrosEspecialidad+'


		GROUP BY Usua_Nombre + '' '' + Usua_Apellido, Espe_Descripcion, Plan_Descripcion
		ORDER BY COUNT(*) DESC')
				
	
END







GO
/****** Object:  StoredProcedure [CHAMBA].[ProfesionalesHoras]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[ProfesionalesHoras] (@Especialidad numeric(18,0), @Semestre int, @Mes int, @Año int)
AS
BEGIN

		DECLARE @parametros varchar(5)
		DECLARE @parametrosEspecialidad varchar(20)

		IF (@Mes = 0)
			IF (@Semestre = 1)
				SET @Parametros = '< 6'
			ELSE
				SET @Parametros = '> 6'
		ELSE
			SET @Parametros = '= ' + CAST(@Mes AS varchar(2))

		IF (@Especialidad = 0)
			SET @ParametrosEspecialidad = ''
		ELSE
			SET @ParametrosEspecialidad = ' AND Espe_Codigo = ' + CAST(@Especialidad AS varchar(18))


		EXEC('SELECT TOP 5 Usua_Nombre + '' '' + Usua_Apellido AS ''Profesional'', Espe_Descripcion AS ''Especialidad'', COUNT(*) / 2 AS ''Cantidad'' 

		FROM CHAMBA.Agenda			
			JOIN CHAMBA.Usuarios ON Agen_Profesional = Usua_Id			
			JOIN CHAMBA.Especialidades ON Agen_Especialidad = Espe_Codigo
		WHERE 
			YEAR(Agen_Fecha) = '+@Año+'
			AND MONTH(Agen_Fecha) '+ @Parametros +'
			'+@ParametrosEspecialidad+'


		GROUP BY Usua_Nombre + '' '' + Usua_Apellido, Espe_Descripcion
		ORDER BY COUNT(*) DESC')
				
END






GO
/****** Object:  StoredProcedure [CHAMBA].[RegistrarAtencion]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [CHAMBA].[RegistrarAtencion](@IdTurno numeric (18,0),@Sintomas varchar(255),@Diagnostico varchar(255), @Fecha datetime)
AS
BEGIN
	INSERT INTO CHAMBA.Consultas(Cons_Turno,Cons_Sintoma,Cons_Diagnostico, Cons_Fecha) VALUES(@IdTurno,@Sintomas,@Diagnostico, @Fecha)
END

GO
/****** Object:  StoredProcedure [CHAMBA].[RegistrarLlegada]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[RegistrarLlegada] (@Turno numeric(18,0), @Bono numeric(18,0), @Fecha datetime)
AS
BEGIN

DECLARE @Paciente numeric(18,0)
SELECT @Paciente = Turn_Paciente FROM CHAMBA.Pacientes JOIN CHAMBA.Turnos ON Turn_Paciente = Paci_Usuario

UPDATE CHAMBA.Bonos SET Bono_Paciente_Uso = @Paciente, Bono_Turno_Uso = @Turno WHERE Bono_Numero = @Bono
UPDATE CHAMBA.Turnos SET Turn_Fecha_Llegada = @Fecha WHERE Turn_Numero = @Turno

END



GO
/****** Object:  StoredProcedure [CHAMBA].[RESERVA_DE_TURNO]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[RESERVA_DE_TURNO]
				(	@Afiliado numeric(18,0),
					@Agenda_id numeric(18,0)
				)
AS
BEGIN
	DECLARE @Nuevo_Id_Turno numeric(18,0), @usuario numeric(18,0)

	/*OBTENCION DE UN NUEVO ID DE TURNO*/
	EXEC CHAMBA.OBTENER_NUEVO_ID_TURNO @Nuevo_Id_Turno OUT

	/*INSERTADO DEL NUEVO TURNO EN LA TABLA TURNOS */
	INSERT INTO CHAMBA.Turnos (Turn_Numero, Turn_Paciente,Turn_Agenda) VALUES (@Nuevo_Id_Turno,@Afiliado,@Agenda_id)

	/*SET OCUPADO EN LA TABLA AGENDA PARA EL DIA Y HORA CORRESPONDIENTE */
	UPDATE CHAMBA.Agenda 
			SET Agen_Ocupado = 1
			WHERE Agen_Id = @Agenda_id
END

GO
/****** Object:  StoredProcedure [CHAMBA].[RolHabilitado]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [CHAMBA].[RolHabilitado] (@nombre nvarchar(30))
as
begin
declare @resultado bit
set @resultado = (select Rol_Estado from CHAMBA.Roles where Rol_Nombre = @nombre)
return @resultado
end




GO
/****** Object:  StoredProcedure [CHAMBA].[TieneConyuge]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[TieneConyuge](@Afiliado numeric(16, 0), @Existe int OUT)
AS
BEGIN
	BEGIN
		SELECT @Existe = COUNT(*) FROM CHAMBA.Pacientes WHERE Paci_Numero = CAST(CAST(@Afiliado as varchar(16)) + '02' AS numeric(18, 0))	
	END
END






GO
/****** Object:  StoredProcedure [CHAMBA].[TurnosCancelablesPorPaciente]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****CANCELACIONES***/
CREATE PROCEDURE [CHAMBA].[TurnosCancelablesPorPaciente] ( @Paciente numeric (18,0), @Fecha datetime)
AS
BEGIN
	SELECT CONVERT(varchar(10), A.Agen_Fecha, 103) + ' ' + CONVERT(varchar(5), A.Agen_Fecha, 108) + ' - ' + Espe_Descripcion AS Turno, T.Turn_Numero
	FROM CHAMBA.Turnos T 
	JOIN CHAMBA.Agenda A ON(A.Agen_Id = T.Turn_Agenda)
	JOIN CHAMBA.Especialidades ON Espe_Codigo = A.Agen_Especialidad
	WHERE T.Turn_Paciente = @Paciente
	AND dateadd(DAY, -1, A.Agen_Fecha) >= @Fecha
	And T.Turn_Cancelado IS NULL
	And A.Agen_Cancelado IS NULL
	ORDER BY A.Agen_Fecha
END

GO
/****** Object:  StoredProcedure [CHAMBA].[VerificarLogin]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[VerificarLogin] (@Usuario varchar(255), @Clave varchar(255), @MaxIntentos numeric(3,0), @Resultado INT OUT, @Id numeric(18,0) OUT)
AS
BEGIN
DECLARE @ClaveEncriptada varchar(255), @Intentos numeric(3,0)
SELECT @Id = Usua_Id, @ClaveEncriptada = Usua_Clave, @Intentos = Usua_Intentos FROM CHAMBA.Usuarios WHERE Usua_Usuario = @Usuario

SELECT @Resultado =
CASE 
	WHEN @Id IS NULL THEN 0
	WHEN @Intentos >= @MaxIntentos THEN 1
	WHEN @ClaveEncriptada <> HASHBYTES('SHA2_256', @Clave) THEN 2
	WHEN (SELECT COUNT(*) FROM CHAMBA.RolesActivos(@Id)) = 0 THEN 3
	ELSE 4
END


SELECT @Intentos = 
	CASE @Resultado
		WHEN 1 THEN @Intentos
		WHEN 2 THEN (@Intentos + 1)
		WHEN 3 THEN 0
		WHEN 4 THEN 0
	END

IF (@Resultado <> 0)
	UPDATE CHAMBA.Usuarios SET Usua_Intentos = @Intentos WHERE Usua_Id = @Id

RETURN
END


/*---------------------------------------CRECION DE TURNOS EN AGENDA--------------------------------------------------*/
SET ANSI_NULLS ON

GO
/****** Object:  Table [CHAMBA].[Agenda]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Agenda](
	[Agen_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Agen_Profesional] [numeric](18, 0) NOT NULL,
	[Agen_Fecha] [datetime] NULL,
	[Agen_Especialidad] [numeric](18, 0) NOT NULL,
	[Agen_Cancelado] [numeric](18, 0) NULL,
	[Agen_Ocupado] [tinyint] NULL,
 CONSTRAINT [PK_Rango_Horario] PRIMARY KEY CLUSTERED 
(
	[Agen_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Bonos]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Bonos](
	[Bono_Numero] [numeric](18, 0) NOT NULL,
	[Bono_Paciente_Uso] [numeric](18, 0) NULL,
	[Bono_Turno_Uso] [numeric](18, 0) NULL,
	[Bono_Compra] [numeric](18, 0) NULL,
	[Bono_Fecha_Impresion] [datetime] NULL,
	[Bono_Valor] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Bono_Consulta] PRIMARY KEY CLUSTERED 
(
	[Bono_Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Cambio_Plan]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Cambio_Plan](
	[Camb_Plan_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Camb_Plan_Paciente] [numeric](18, 0) NOT NULL,
	[Camb_Plan_Plan_Anterior] [numeric](18, 0) NOT NULL,
	[Camb_Plan_Plan_Nuevo] [numeric](18, 0) NOT NULL,
	[Camb_Plan_Fecha] [datetime] NULL,
	[Camb_Plan_Razon] [varchar](255) NULL,
 CONSTRAINT [PK_Cambio_Plan] PRIMARY KEY CLUSTERED 
(
	[Camb_Plan_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CHAMBA].[Cancelaciones]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Cancelaciones](
	[Canc_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Canc_Descripcion] [varchar](255) NULL,
	[Canc_Tipo] [numeric](1, 0) NULL,
 CONSTRAINT [PK_Cancelaciones] PRIMARY KEY CLUSTERED 
(
	[Canc_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CHAMBA].[Compra_Bonos]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Compra_Bonos](
	[Comp_Bono_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Comp_Bono_Fecha] [datetime] NULL,
	[Comp_Bono_Plan] [numeric](18, 0) NOT NULL,
	[Comp_Bono_Paciente] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Compra_Bono] PRIMARY KEY CLUSTERED 
(
	[Comp_Bono_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Consultas]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Consultas](
	[Cons_Turno] [numeric](18, 0) NOT NULL,
	[Cons_Sintoma] [varchar](255) NULL,
	[Cons_Diagnostico] [varchar](255) NULL,
	[Cons_Fecha] [datetime] NULL,
 CONSTRAINT [PK_Consultas] PRIMARY KEY CLUSTERED 
(
	[Cons_Turno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CHAMBA].[Especialidades]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Especialidades](
	[Espe_Codigo] [numeric](18, 0) NOT NULL,
	[Espe_Descripcion] [varchar](255) NULL,
 CONSTRAINT [PK_Especialidades] PRIMARY KEY CLUSTERED 
(
	[Espe_Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CHAMBA].[Funcionalidad_X_Rol]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Funcionalidad_X_Rol](
	[Func_X_Rol_Rol] [numeric](18, 0) NOT NULL,
	[Func_X_Rol_Funcionalidad] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Funcionalidad_X_Rol] PRIMARY KEY CLUSTERED 
(
	[Func_X_Rol_Rol] ASC,
	[Func_X_Rol_Funcionalidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Funcionalidades]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Funcionalidades](
	[Func_Id] [numeric](18, 0) NOT NULL,
	[Func_Descripcion] [varchar](255) NULL,
 CONSTRAINT [PK_Funcionalidades] PRIMARY KEY CLUSTERED 
(
	[Func_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CHAMBA].[Pacientes]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Pacientes](
	[Paci_Usuario] [numeric](18, 0) NOT NULL,
	[Paci_Numero] [numeric](18, 0) NULL,
	[Paci_Estado_Civil] [numeric](2, 0) NULL,
	[Paci_Cant_Hijos] [numeric](3, 0) NULL,
	[Paci_Plan] [numeric](18, 0) NOT NULL,
	[Paci_Fecha_Baja] [datetime] NULL,
 CONSTRAINT [PK_Pacientes] PRIMARY KEY CLUSTERED 
(
	[Paci_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Planes]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Planes](
	[Plan_Codigo] [numeric](18, 0) NOT NULL,
	[Plan_Descripcion] [varchar](255) NULL,
	[Plan_Precio_Bono_Consulta] [numeric](18, 0) NULL,
	[Plan_Precio_Bono_Farmacia] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Planes] PRIMARY KEY CLUSTERED 
(
	[Plan_Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CHAMBA].[Profesionales]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Profesionales](
	[Prof_Usuario] [numeric](18, 0) NOT NULL,
	[Prof_Matricula] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Profesionales] PRIMARY KEY CLUSTERED 
(
	[Prof_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Rol_X_Usuario]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Rol_X_Usuario](
	[Rol_X_Usua_Usuario] [numeric](18, 0) NOT NULL,
	[Rol_X_Usua_Rol] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Rol_X_Usuario] PRIMARY KEY CLUSTERED 
(
	[Rol_X_Usua_Usuario] ASC,
	[Rol_X_Usua_Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Roles]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Roles](
	[Rol_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Rol_Nombre] [varchar](255) NULL,
	[Rol_Estado] [numeric](3, 0) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Rol_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CHAMBA].[Tipo_Especialidad]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Tipo_Especialidad](
	[Tipo_Espe_Codigo] [numeric](18, 0) NOT NULL,
	[Tipo_Espe_Descripcion] [varchar](255) NULL,
	[Tipo_Espe_Especialidad] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Tipo_Especialidad] PRIMARY KEY CLUSTERED 
(
	[Tipo_Espe_Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CHAMBA].[Tipo_Especialidad_X_Profesional]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Tipo_Especialidad_X_Profesional](
	[Tipo_Espe_X_Prof_Profesional] [numeric](18, 0) NOT NULL,
	[Tipo_Espe_X_Prof_Tipo_Especialidad] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Especialidad_X_Profesional] PRIMARY KEY CLUSTERED 
(
	[Tipo_Espe_X_Prof_Profesional] ASC,
	[Tipo_Espe_X_Prof_Tipo_Especialidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Turnos]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHAMBA].[Turnos](
	[Turn_Numero] [numeric](18, 0) NOT NULL,
	[Turn_Paciente] [numeric](18, 0) NOT NULL,
	[Turn_Fecha_Llegada] [datetime] NULL,
	[Turn_Cancelado] [numeric](18, 0) NULL,
	[Turn_Agenda] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Turnos] PRIMARY KEY CLUSTERED 
(
	[Turn_Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [CHAMBA].[Usuarios]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [CHAMBA].[Usuarios](
	[Usua_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Usua_DNI] [numeric](18, 0) NULL,
	[Usua_TipoDNI] [numeric](3, 0) NULL,
	[Usua_Nombre] [varchar](255) NULL,
	[Usua_Apellido] [varchar](255) NULL,
	[Usua_Direccion] [varchar](255) NULL,
	[Usua_Telefono] [numeric](18, 0) NULL,
	[Usua_Mail] [varchar](255) NULL,
	[Usua_Fecha_Nac] [datetime] NULL,
	[Usua_Sexo] [char](1) NULL,
	[Usua_Usuario] [varchar](255) NULL,
	[Usua_Clave] [varchar](255) NULL,
	[Usua_Intentos] [numeric](3, 0) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Usua_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [CHAMBA].[RolesActivos]    Script Date: 2/11/2016 1:28:57 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [CHAMBA].[RolesActivos](@Id numeric(18,0))
RETURNS TABLE
AS

RETURN (SELECT Rol_Id, Rol_Nombre
FROM CHAMBA.Rol_X_Usuario JOIN CHAMBA.Pacientes ON Rol_X_Usua_Usuario = Paci_Usuario JOIN CHAMBA.Roles ON Rol_Id = Rol_X_Usua_Rol
WHERE Rol_X_Usua_Usuario = @Id AND Rol_Estado = 1 AND Paci_Fecha_Baja IS NULL

UNION 

SELECT Rol_Id, Rol_Nombre
FROM CHAMBA.Rol_X_Usuario JOIN CHAMBA.Roles ON Rol_Id = Rol_X_Usua_Rol
WHERE Rol_X_Usua_Usuario = @Id AND (SELECT COUNT(*) FROM CHAMBA.Pacientes WHERE Paci_Usuario = @Id) = 0 AND Rol_Estado = 1)









GO
ALTER TABLE [CHAMBA].[Agenda]  WITH CHECK ADD  CONSTRAINT [FK_Agenda_Cancelaciones] FOREIGN KEY([Agen_Cancelado])
REFERENCES [CHAMBA].[Cancelaciones] ([Canc_Id])
GO
ALTER TABLE [CHAMBA].[Agenda] CHECK CONSTRAINT [FK_Agenda_Cancelaciones]
GO
ALTER TABLE [CHAMBA].[Agenda]  WITH CHECK ADD  CONSTRAINT [FK_Agenda_Especialidad] FOREIGN KEY([Agen_Especialidad])
REFERENCES [CHAMBA].[Especialidades] ([Espe_Codigo])
GO
ALTER TABLE [CHAMBA].[Agenda] CHECK CONSTRAINT [FK_Agenda_Especialidad]
GO
ALTER TABLE [CHAMBA].[Agenda]  WITH CHECK ADD  CONSTRAINT [FK_Agenda_Profesionales] FOREIGN KEY([Agen_Profesional])
REFERENCES [CHAMBA].[Profesionales] ([Prof_Usuario])
GO
ALTER TABLE [CHAMBA].[Agenda] CHECK CONSTRAINT [FK_Agenda_Profesionales]
GO
ALTER TABLE [CHAMBA].[Bonos]  WITH CHECK ADD  CONSTRAINT [FK_Bonos_Compra_Bonos] FOREIGN KEY([Bono_Compra])
REFERENCES [CHAMBA].[Compra_Bonos] ([Comp_Bono_Id])
GO
ALTER TABLE [CHAMBA].[Bonos] CHECK CONSTRAINT [FK_Bonos_Compra_Bonos]
GO
ALTER TABLE [CHAMBA].[Bonos]  WITH CHECK ADD  CONSTRAINT [FK_Bonos_Consulta_Turno] FOREIGN KEY([Bono_Turno_Uso])
REFERENCES [CHAMBA].[Turnos] ([Turn_Numero])
GO
ALTER TABLE [CHAMBA].[Bonos] CHECK CONSTRAINT [FK_Bonos_Consulta_Turno]
GO
ALTER TABLE [CHAMBA].[Bonos]  WITH CHECK ADD  CONSTRAINT [FK_Bonos_Pacientes] FOREIGN KEY([Bono_Paciente_Uso])
REFERENCES [CHAMBA].[Pacientes] ([Paci_Usuario])
GO
ALTER TABLE [CHAMBA].[Bonos] CHECK CONSTRAINT [FK_Bonos_Pacientes]
GO
ALTER TABLE [CHAMBA].[Cambio_Plan]  WITH CHECK ADD  CONSTRAINT [FK_Cambio_Plan_Pacientes] FOREIGN KEY([Camb_Plan_Paciente])
REFERENCES [CHAMBA].[Pacientes] ([Paci_Usuario])
GO
ALTER TABLE [CHAMBA].[Cambio_Plan] CHECK CONSTRAINT [FK_Cambio_Plan_Pacientes]
GO
ALTER TABLE [CHAMBA].[Cambio_Plan]  WITH CHECK ADD  CONSTRAINT [FK_Cambio_Plan_Plan] FOREIGN KEY([Camb_Plan_Plan_Anterior])
REFERENCES [CHAMBA].[Planes] ([Plan_Codigo])
GO
ALTER TABLE [CHAMBA].[Cambio_Plan] CHECK CONSTRAINT [FK_Cambio_Plan_Plan]
GO
ALTER TABLE [CHAMBA].[Cambio_Plan]  WITH CHECK ADD  CONSTRAINT [FK_Cambio_Plan_Plan1] FOREIGN KEY([Camb_Plan_Plan_Nuevo])
REFERENCES [CHAMBA].[Planes] ([Plan_Codigo])
GO
ALTER TABLE [CHAMBA].[Cambio_Plan] CHECK CONSTRAINT [FK_Cambio_Plan_Plan1]
GO
ALTER TABLE [CHAMBA].[Compra_Bonos]  WITH CHECK ADD  CONSTRAINT [FK_Compra_Bono_Plan] FOREIGN KEY([Comp_Bono_Plan])
REFERENCES [CHAMBA].[Planes] ([Plan_Codigo])
GO
ALTER TABLE [CHAMBA].[Compra_Bonos] CHECK CONSTRAINT [FK_Compra_Bono_Plan]
GO
ALTER TABLE [CHAMBA].[Compra_Bonos]  WITH CHECK ADD  CONSTRAINT [FK_Compra_Bonos_Pacientes] FOREIGN KEY([Comp_Bono_Paciente])
REFERENCES [CHAMBA].[Pacientes] ([Paci_Usuario])
GO
ALTER TABLE [CHAMBA].[Compra_Bonos] CHECK CONSTRAINT [FK_Compra_Bonos_Pacientes]
GO
ALTER TABLE [CHAMBA].[Consultas]  WITH CHECK ADD  CONSTRAINT [FK_Consultas_Turnos] FOREIGN KEY([Cons_Turno])
REFERENCES [CHAMBA].[Turnos] ([Turn_Numero])
GO
ALTER TABLE [CHAMBA].[Consultas] CHECK CONSTRAINT [FK_Consultas_Turnos]
GO
ALTER TABLE [CHAMBA].[Funcionalidad_X_Rol]  WITH CHECK ADD  CONSTRAINT [FK_Funcionalidad_X_Rol_Funcionalidad] FOREIGN KEY([Func_X_Rol_Funcionalidad])
REFERENCES [CHAMBA].[Funcionalidades] ([Func_Id])
GO
ALTER TABLE [CHAMBA].[Funcionalidad_X_Rol] CHECK CONSTRAINT [FK_Funcionalidad_X_Rol_Funcionalidad]
GO
ALTER TABLE [CHAMBA].[Funcionalidad_X_Rol]  WITH CHECK ADD  CONSTRAINT [FK_Funcionalidad_X_Rol_Rol] FOREIGN KEY([Func_X_Rol_Rol])
REFERENCES [CHAMBA].[Roles] ([Rol_Id])
GO
ALTER TABLE [CHAMBA].[Funcionalidad_X_Rol] CHECK CONSTRAINT [FK_Funcionalidad_X_Rol_Rol]
GO
ALTER TABLE [CHAMBA].[Pacientes]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_Plan] FOREIGN KEY([Paci_Plan])
REFERENCES [CHAMBA].[Planes] ([Plan_Codigo])
GO
ALTER TABLE [CHAMBA].[Pacientes] CHECK CONSTRAINT [FK_Paciente_Plan]
GO
ALTER TABLE [CHAMBA].[Pacientes]  WITH CHECK ADD  CONSTRAINT [FK_Pacientes_Usuarios] FOREIGN KEY([Paci_Usuario])
REFERENCES [CHAMBA].[Usuarios] ([Usua_Id])
GO
ALTER TABLE [CHAMBA].[Pacientes] CHECK CONSTRAINT [FK_Pacientes_Usuarios]
GO
ALTER TABLE [CHAMBA].[Profesionales]  WITH CHECK ADD  CONSTRAINT [FK_Profesionales_Usuarios] FOREIGN KEY([Prof_Usuario])
REFERENCES [CHAMBA].[Usuarios] ([Usua_Id])
GO
ALTER TABLE [CHAMBA].[Profesionales] CHECK CONSTRAINT [FK_Profesionales_Usuarios]
GO
ALTER TABLE [CHAMBA].[Rol_X_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Rol_X_Usuario_Rol] FOREIGN KEY([Rol_X_Usua_Rol])
REFERENCES [CHAMBA].[Roles] ([Rol_Id])
GO
ALTER TABLE [CHAMBA].[Rol_X_Usuario] CHECK CONSTRAINT [FK_Rol_X_Usuario_Rol]
GO
ALTER TABLE [CHAMBA].[Rol_X_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Rol_X_Usuario_Usuarios] FOREIGN KEY([Rol_X_Usua_Usuario])
REFERENCES [CHAMBA].[Usuarios] ([Usua_Id])
GO
ALTER TABLE [CHAMBA].[Rol_X_Usuario] CHECK CONSTRAINT [FK_Rol_X_Usuario_Usuarios]
GO
ALTER TABLE [CHAMBA].[Tipo_Especialidad]  WITH CHECK ADD  CONSTRAINT [FK_Tipo_Especialidad_Especialidad] FOREIGN KEY([Tipo_Espe_Especialidad])
REFERENCES [CHAMBA].[Especialidades] ([Espe_Codigo])
GO
ALTER TABLE [CHAMBA].[Tipo_Especialidad] CHECK CONSTRAINT [FK_Tipo_Especialidad_Especialidad]
GO
ALTER TABLE [CHAMBA].[Tipo_Especialidad_X_Profesional]  WITH CHECK ADD  CONSTRAINT [FK_Tipo_Especialidad_X_Profesional_Profesionales] FOREIGN KEY([Tipo_Espe_X_Prof_Profesional])
REFERENCES [CHAMBA].[Profesionales] ([Prof_Usuario])
GO
ALTER TABLE [CHAMBA].[Tipo_Especialidad_X_Profesional] CHECK CONSTRAINT [FK_Tipo_Especialidad_X_Profesional_Profesionales]
GO
ALTER TABLE [CHAMBA].[Tipo_Especialidad_X_Profesional]  WITH CHECK ADD  CONSTRAINT [FK_Tipo_Especialidad_X_Profesional_Tipo_Especialidad] FOREIGN KEY([Tipo_Espe_X_Prof_Tipo_Especialidad])
REFERENCES [CHAMBA].[Tipo_Especialidad] ([Tipo_Espe_Codigo])
GO
ALTER TABLE [CHAMBA].[Tipo_Especialidad_X_Profesional] CHECK CONSTRAINT [FK_Tipo_Especialidad_X_Profesional_Tipo_Especialidad]
GO
ALTER TABLE [CHAMBA].[Turnos]  WITH CHECK ADD  CONSTRAINT [FK_Turnos_Agenda] FOREIGN KEY([Turn_Agenda])
REFERENCES [CHAMBA].[Agenda] ([Agen_Id])
GO
ALTER TABLE [CHAMBA].[Turnos] CHECK CONSTRAINT [FK_Turnos_Agenda]
GO
ALTER TABLE [CHAMBA].[Turnos]  WITH CHECK ADD  CONSTRAINT [FK_Turnos_Cancelaciones] FOREIGN KEY([Turn_Cancelado])
REFERENCES [CHAMBA].[Cancelaciones] ([Canc_Id])
GO
ALTER TABLE [CHAMBA].[Turnos] CHECK CONSTRAINT [FK_Turnos_Cancelaciones]
GO
ALTER TABLE [CHAMBA].[Turnos]  WITH CHECK ADD  CONSTRAINT [FK_Turnos_Pacientes] FOREIGN KEY([Turn_Paciente])
REFERENCES [CHAMBA].[Pacientes] ([Paci_Usuario])
GO
ALTER TABLE [CHAMBA].[Turnos] CHECK CONSTRAINT [FK_Turnos_Pacientes]
GO


/* -----------------------------------------------------------MIGRACION---------------------------------------------------------------------------- */

CREATE PROCEDURE CHAMBA.Migracion 

AS
BEGIN

/* CREACION DE ROLES */

INSERT INTO CHAMBA.Roles (Rol_Nombre, Rol_Estado) VALUES ('Administrativo', 1), ('Profesional', 1), ('Afiliado', 1)

INSERT INTO CHAMBA.Usuarios (Usua_Usuario, Usua_Clave, Usua_Nombre, Usua_Intentos) VALUES ('admin', HASHBYTES('SHA2_256', 'w23e'), 'Administrador General', 0)

INSERT INTO CHAMBA.Rol_X_Usuario(Rol_X_Usua_Usuario, Rol_X_Usua_Rol) VALUES ((SELECT Usua_Id FROM CHAMBA.Usuarios WHERE Usua_Usuario = 'admin'), (SELECT Rol_Id FROM CHAMBA.Roles WHERE Rol_Nombre = 'Administrativo'))

/* CREACION DE FUNCIONALIDADES */
INSERT INTO CHAMBA.Funcionalidades (Func_Id, Func_Descripcion) VALUES (1, 'Gestionar roles'), (2, 'Gestionar afiliados'), (3, 'Comprar bonos'), (4, 'Pedir turnos'), (5, 'Registrar llegada para atencion'), (6, 'Registrar resultado de atencion'), (7, 'Cancelar turnos'), (8, 'Estadisticas'), (9, 'Registrar agenda')

/* ASIGNACION DE FUNCIONALIDADES A ROLES*/
DECLARE @Rol numeric(18,0) = (SELECT Rol_Id FROM CHAMBA.Roles WHERE Rol_Nombre = 'Administrativo')

INSERT INTO CHAMBA.Funcionalidad_X_Rol (Func_X_Rol_Rol, Func_X_Rol_Funcionalidad) VALUES (@Rol, 1), (@Rol, 2), (@Rol, 3), (@Rol, 4), (@Rol, 5), (@Rol, 6), (@Rol, 7), (@Rol, 8), (@Rol, 9)

SET @Rol = (SELECT Rol_Id FROM CHAMBA.Roles WHERE Rol_Nombre = 'Profesional')
INSERT INTO CHAMBA.Funcionalidad_X_Rol (Func_X_Rol_Rol, Func_X_Rol_Funcionalidad) VALUES (@Rol, 6), (@Rol, 7), (@Rol, 9)

SET @Rol = (SELECT Rol_Id FROM CHAMBA.Roles WHERE Rol_Nombre = 'Afiliado')
INSERT INTO CHAMBA.Funcionalidad_X_Rol (Func_X_Rol_Rol, Func_X_Rol_Funcionalidad) VALUES (@Rol, 3), (@Rol, 4), (@Rol, 7)

/* MIGRACION DE ESPECIALIDADES */

INSERT INTO CHAMBA.Especialidades (Espe_Codigo, Espe_Descripcion)
SELECT DISTINCT Especialidad_Codigo, Especialidad_Descripcion 
FROM gd_esquema.Maestra
WHERE Especialidad_Codigo IS NOT NULL

/* MIGRACION DE TIPOS DE ESPECIALIDAD */

INSERT INTO CHAMBA.Tipo_Especialidad (Tipo_Espe_Codigo, Tipo_Espe_Descripcion, Tipo_Espe_Especialidad)
SELECT CAST(CAST(Especialidad_Codigo AS VARCHAR(18)) + CAST(Tipo_Especialidad_Codigo AS VARCHAR(18)) AS NUMERIC(18,0)), Tipo_Especialidad_Descripcion, Especialidad_Codigo 
FROM gd_esquema.Maestra
WHERE Tipo_Especialidad_Codigo IS NOT NULL
GROUP BY Tipo_Especialidad_Codigo, Tipo_Especialidad_Descripcion, Especialidad_Codigo 

/* MIGRACION DE PLANES */

INSERT INTO CHAMBA.Planes (Plan_Codigo, Plan_Descripcion, Plan_Precio_Bono_Consulta, Plan_Precio_Bono_Farmacia)
SELECT DISTINCT Plan_Med_Codigo, Plan_Med_Descripcion, Plan_Med_Precio_Bono_Consulta, Plan_Med_Precio_Bono_Farmacia 
FROM gd_esquema.Maestra
WHERE Plan_Med_Codigo IS NOT NULL


/* DECLARACION DE VARIABLES PARA CURSORES */

DECLARE @DNI numeric(18,0), @Nombre varchar(255), @Apellido varchar(255), @Direccion varchar(255), @Telefono numeric(18,0), @Mail varchar(255), @Fecha_Nac datetime
DECLARE @Plan numeric(18,0)
DECLARE @Existe numeric(18,0)

/* DECLARACION DE CURSOR DE PACIENTES */

DECLARE cursorPacientes CURSOR FOR SELECT DISTINCT Paciente_DNI, Paciente_Nombre, Paciente_Apellido, Paciente_Direccion, Paciente_Telefono, Paciente_Mail, Paciente_Fecha_Nac, Plan_Med_Codigo
FROM gd_esquema.Maestra
WHERE Paciente_DNI IS NOT NULL


/* MIGRACION DE PACIENTES */

SET @Rol = (SELECT Rol_Id FROM CHAMBA.Roles WHERE Rol_Nombre = 'Afiliado')

OPEN cursorPacientes
FETCH NEXT FROM cursorPacientes INTO @DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac, @Plan
WHILE @@FETCH_STATUS=0
BEGIN

SET @Existe = NULL

SELECT @Existe = Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = @DNI

IF (@Existe IS NULL) 
	BEGIN
		INSERT INTO CHAMBA.Usuarios (Usua_DNI, Usua_TipoDNI, Usua_Nombre, Usua_Apellido, Usua_Direccion, Usua_Telefono, Usua_Mail, Usua_Fecha_Nac, Usua_Sexo, Usua_Usuario, Usua_Clave, Usua_Intentos)
		VALUES (@DNI, 0, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac, 'M', @Mail, HASHBYTES('SHA2_256', CAST(@DNI AS VARCHAR(18))), 0)
		SET @Existe = @@IDENTITY
	END

INSERT INTO CHAMBA.Pacientes (Paci_Usuario, Paci_Numero, Paci_Estado_Civil, Paci_Cant_Hijos, Paci_Plan) VALUES (@Existe, CAST(CAST(@Existe as varchar(16)) + '01' AS numeric(18,0)), 0, 0, @Plan)

INSERT INTO CHAMBA.Rol_X_Usuario (Rol_X_Usua_Usuario, Rol_X_Usua_Rol) VALUES (@Existe, @Rol)

FETCH NEXT FROM cursorPacientes INTO @DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac, @Plan
END
CLOSE cursorPacientes
DEALLOCATE cursorPacientes

/* DECLARACION DE CURSOR DE PROFESIONALES */

DECLARE cursorMedicos CURSOR FOR SELECT DISTINCT Medico_DNI, Medico_Nombre, Medico_Apellido, Medico_Direccion, Medico_Telefono, Medico_Mail, Medico_Fecha_Nac
FROM gd_esquema.Maestra
WHERE Medico_DNI IS NOT NULL


/* MIGRACION DE PROFESIONALES */

SET @Rol = (SELECT Rol_Id FROM CHAMBA.Roles WHERE Rol_Nombre = 'Profesional')

OPEN cursorMedicos
FETCH NEXT FROM cursorMedicos INTO @DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac
WHILE @@FETCH_STATUS=0
BEGIN

SET @Existe = NULL

SELECT @Existe = Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = @DNI

IF (@Existe IS NULL)
	BEGIN
		INSERT INTO CHAMBA.Usuarios (Usua_DNI, Usua_TipoDNI, Usua_Nombre, Usua_Apellido, Usua_Direccion, Usua_Telefono, Usua_Mail, Usua_Fecha_Nac, Usua_Sexo, Usua_Usuario, Usua_Clave, Usua_Intentos)
		VALUES (@DNI, 0, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac, 'M', @Mail, HASHBYTES('SHA2_256', CAST(@DNI AS VARCHAR(18))), 0)
		SET @Existe = @@IDENTITY
	END

INSERT INTO CHAMBA.Profesionales (Prof_Usuario) VALUES (@Existe)

INSERT INTO CHAMBA.Rol_X_Usuario (Rol_X_Usua_Usuario, Rol_X_Usua_Rol) VALUES (@Existe, @Rol)


FETCH NEXT FROM cursorMedicos INTO @DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac
END
CLOSE cursorMedicos
DEALLOCATE cursorMedicos


/* MIGRACION DE TIPO_ESPECIALIDAD_X_PROFESIONAL */

INSERT INTO CHAMBA.Tipo_Especialidad_X_Profesional (Tipo_Espe_X_Prof_Tipo_Especialidad, Tipo_Espe_X_Prof_Profesional)
SELECT CAST(CAST(Especialidad_Codigo AS VARCHAR(18)) + CAST(Tipo_Especialidad_Codigo AS VARCHAR(18)) AS NUMERIC(18,0)), (SELECT Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = Medico_DNI) FROM
(SELECT DISTINCT Especialidad_Codigo, Tipo_Especialidad_Codigo, Medico_DNI
FROM gd_esquema.Maestra
WHERE Tipo_Especialidad_Codigo IS NOT NULL) AS S1


/* MIGRACION DE AGENDA Y TURNOS */

INSERT INTO CHAMBA.Agenda (Agen_Fecha, Agen_Profesional, Agen_Especialidad, Agen_Ocupado)
SELECT DISTINCT Turno_Fecha, (SELECT Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = Medico_DNI), Especialidad_Codigo, 1
FROM gd_esquema.Maestra
WHERE Turno_Numero IS NOT NULL

INSERT INTO CHAMBA.Turnos (Turn_Numero, Turn_Paciente, Turn_Agenda)
SELECT DISTINCT Turno_Numero, (SELECT Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = Paciente_DNI), (SELECT Agen_Id FROM CHAMBA.Agenda WHERE 
Agen_Fecha = Turno_Fecha AND Agen_Profesional = (SELECT Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = Medico_DNI) AND Agen_Especialidad = Especialidad_Codigo
)
FROM gd_esquema.Maestra
WHERE Turno_Numero IS NOT NULL

/* MIGRACION DE CONSULTAS */

INSERT INTO CHAMBA.Consultas (Cons_Turno, Cons_Sintoma, Cons_Diagnostico)
SELECT Turno_Numero, Consulta_Sintomas, Consulta_Enfermedades
FROM gd_esquema.Maestra
WHERE Consulta_Sintomas IS NOT NULL


/* MIGRACION DE COMPRA DE BONOS */

INSERT INTO CHAMBA.Compra_Bonos(Comp_Bono_Fecha, Comp_Bono_Paciente, Comp_Bono_Plan)
SELECT DISTINCT Compra_Bono_Fecha, (SELECT Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = Paciente_DNI), Plan_Med_Codigo
FROM gd_esquema.Maestra
WHERE Compra_Bono_Fecha IS NOT NULL

/* MIGRACION DE BONOS */

INSERT INTO CHAMBA.Bonos(Bono_Compra, Bono_Numero, Bono_Valor, Bono_Fecha_Impresion)
SELECT DISTINCT (SELECT Comp_Bono_Id FROM CHAMBA.Compra_Bonos WHERE Comp_Bono_Fecha = Compra_Bono_Fecha AND Comp_Bono_Paciente = (SELECT Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = Paciente_DNI) AND Comp_Bono_Plan = Plan_Med_Codigo), Bono_Consulta_Numero, Plan_Med_Precio_Bono_Consulta, Bono_Consulta_Fecha_Impresion
FROM gd_esquema.Maestra
WHERE Compra_Bono_Fecha IS NOT NULL

UPDATE CHAMBA.Bonos SET Bono_Turno_Uso = i.Turno_Numero, Bono_Paciente_Uso = (SELECT Usua_Id FROM CHAMBA.Usuarios WHERE Usua_DNI = i.Paciente_DNI)
FROM 
	(SELECT Turno_Numero, Paciente_DNI, Bono_Consulta_Numero FROM gd_esquema.Maestra) AS i 
WHERE CHAMBA.Bonos.Bono_Numero = i.Bono_Consulta_Numero 

END
GO
EXEC CHAMBA.Migracion