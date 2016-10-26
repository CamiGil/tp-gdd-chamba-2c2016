/*-----------------------------------------------VERIFICACION DE UN BONO----------------------------------------------*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[VerificarBono] 
				(	@numero_bono numeric(18, 0),
					@Afiliado numeric(18, 0),
					@resultado INT OUT
				)
AS
BEGIN
	IF ((SELECT COUNT(*)
			FROM CHAMBA.Bonos B
				join CHAMBA.Compra_Bonos c on b.Bono_Compra = c.Comp_Bono_Id
				join CHAMBA.Pacientes p on p.Paci_Numero = c.Comp_Bono_Paciente
			WHERE	CAST(p.Paci_Numero as varchar(16)) = CAST(@Afiliado as varchar(16))
				and	b.Bono_Numero = @numero_bono
			) >0
		)
		BEGIN
			SET @resultado = 0
		END
	ELSE 
		BEGIN 
			SET @resultado = 1
		END
END
GO

/*---------------------------------------ESPECIALIDADES DE UN PROFESIONAL----------------------------------------------*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[BUSQUEDA_PROFESIONAL_ESPECIALIDADES] (@Nombre_profesional varchar(510), @Apellido_profesional varchar(510))
AS
BEGIN
	SELECT	te.Tipo_Espe_Codigo as 'Codigo de la especialidad', 
			te.Tipo_Espe_Descripcion as 'Tipo especialidad',
			e.Espe_Descripcion as 'Especialdiad'
		FROM	 CHAMBA.Usuarios u
			join CHAMBA.Profesionales p on p.Prof_Usuario = u.Usua_Id
			join CHAMBA.Tipo_Especialdiad_X_Profesional tep on tep.Tipo_Espe_X_Prof_Profesional = p.Prof_Usuario
			join CHAMBA.Tipo_Especialidad te on te.Tipo_Espe_Codigo = tep.Tipo_Espe_X_Prof_Tipo_Especialidad
			join CHAMBA.Especialidades e on e.Espe_codigo = te.Tipo_Espe_Especialidad
		WHERE	u.Usua_Nombre = @Nombre_profesional 
			and u.Usua_Apellido = @Apellido_profesional
END
GO

/*-----------------------------------ESPECIALIDADES DE UN PROFESIONAL REFINADA POR TIPO------------------------------------*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[BUSQUEDA_PROFESIONAL_REFINADO_POR_TIPO] 
				(	@Nombre_profesional varchar(510),
					@Apellido_profesional varchar(510),
					@Tipo_Especialidad varchar(510)
				)
AS
BEGIN
	SELECT	te.Tipo_Espe_Codigo as 'Codigo de la especialidad', 
			e.Espe_Descripcion as 'Especialdiad'
		FROM	 CHAMBA.Usuarios u
			join CHAMBA.Profesionales p on p.Prof_Usuario = u.Usua_Id
			join CHAMBA.Tipo_Especialdiad_X_Profesional tep on tep.Tipo_Espe_X_Prof_Profesional = p.Prof_Usuario 
			join CHAMBA.Tipo_Especialidad te on te.Tipo_Espe_Codigo = tep.Tipo_Espe_X_Prof_Tipo_Especialidad
			join CHAMBA.Especialidades e on e.Espe_codigo = te.Tipo_Espe_Especialidad
		WHERE	u.Usua_Nombre = @Nombre_profesional 
			and u.Usua_Apellido = @Apellido_profesional
			and te.Tipo_Espe_Descripcion = @Tipo_Especialidad
END
GO

/*---------------------------------------DIAS DISPONIBLES DE UN PROF SEGUN ESPECIALIDAD-----------------------------------------*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[DIAS_DISPONIBLES_PROFESIONAL_POR_ESPECIALDIAD] 
				(	@Nombre_profesional varchar(510),
					@Apellido_profesional varchar(510),
					@Tipo_especialidad varchar(510),
					@Especialidad varchar(510),
					@Numero_mes INT,
					@Anio INT
				)
AS 
BEGIN
	SELECT DAY(a.Agen_Fecha) as 'Dia',
			MONTH(a.Agen_Fecha) as 'Mes'
		FROM	 CHAMBA.Agenda a
			join CHAMBA.Profesionales p on a.Agen_Profesional = p.Prof_Usuario 
			join CHAMBA.Usuarios u on u.Usua_Id = p.Prof_Usuario
			join CHAMBA.Tipo_Especialidad te on te.Tipo_Espe_Codigo = a.Agen_Tipo_Especialidad
			join CHAMBA.Especialidades e on e.Espe_Codigo = te.Tipo_Espe_Especialidad
		WHERE	u.Usua_Nombre = @Nombre_profesional
			and u.Usua_Apellido = @Apellido_profesional
			and te.Tipo_Espe_Descripcion = @Tipo_especialidad
			and e.Espe_Descripcion = @Especialidad
			and MONTH(a.Agen_Fecha) = @Numero_Mes
			and YEAR(a.Agen_Fecha) = @Anio
			and a.Agen_Cancelado IS NULL
			and a.Agen_Ocupado IS NULL /*<-- AGREGAR ESTE CAMPO EN LA TABLA AGENDA */
END
GO

/*---------------------------------------HORARIOS DISPONIBLES EN UN DIA--------------------------------------------------*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[HORARIOS_DISPONIBLES_EN_AGENDA_PROFESIONAL] 
				(	@Nombre_profesional varchar(510),
					@Apellido_profesional varchar(510),
					@Tipo_especialidad varchar(510),
					@Especialidad varchar(510),
					@Dia INT,
					@Numero_mes INT,
					@Anio INT	
				)
AS
BEGIN
	SELECT	DATEPART(hh,a.Agen_Fecha) as 'Hora',
			DATEPART(mi,a.Agen_Fecha) as 'Minutos'
		FROM	 CHAMBA.Agenda a
			join CHAMBA.Profesionales p on a.Agen_Profesional = p.Prof_Usuario 
			join CHAMBA.Usuarios u on u.Usua_Id = p.Prof_Usuario
			join CHAMBA.Tipo_Especialidad te on te.Tipo_Espe_Codigo = a.Agen_Tipo_Especialidad
			join CHAMBA.Especialidades e on e.Espe_Codigo = te.Tipo_Espe_Especialidad
		WHERE	u.Usua_Nombre = @Nombre_profesional
			and u.Usua_Apellido = @Apellido_profesional
			and te.Tipo_Espe_Descripcion = @Tipo_especialidad
			and e.Espe_Descripcion = @Especialidad
			and YEAR(a.Agen_Fecha) = @Anio
			and MONTH(a.Agen_Fecha) = @Numero_Mes
			and DAY(a.Agen_Fecha) = @Dia
END
GO

/*---------------------------------------RESERVA DE TURNO----------------------------------------------------------*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [CHAMBA].[RESERVA_DE_TURNO]
				(	@Afiliado numeric(18,0),
					@Numero_bono numeric(18,0),
					@Nombre_profesional varchar(510),
					@Apellido_profesional varchar(510),
					@Tipo_especialidad varchar(510),
					@Especialidad varchar(510),
					@Dia INT,
					@Numero_mes INT,
					@Anio INT,
					@Hora INT,
					@Minuto INT
				)
AS
BEGIN
	DECLARE @Agenda_id numeric(18,0), @Nuevo_Id_Turno numeric(18,0)

	/*OBTENCION DEL ID DE LA AGENDA*/
	SELECT @Agenda_id = a.Agen_Id 
		FROM	 CHAMBA.Agenda a
			join CHAMBA.Profesionales p on a.Agen_Profesional = p.Prof_Usuario 
			join CHAMBA.Usuarios u on u.Usua_Id = p.Prof_Usuario
			join CHAMBA.Tipo_Especialidad te on te.Tipo_Espe_Codigo = a.Agen_Tipo_Especialidad
			join CHAMBA.Especialidades e on e.Espe_Codigo = te.Tipo_Espe_Especialidad
		WHERE	u.Usua_Nombre = @Nombre_profesional
			and u.Usua_Apellido = @Apellido_profesional
			and te.Tipo_Espe_Descripcion = @Tipo_especialidad
			and e.Espe_Descripcion = @Especialidad
			and	YEAR(a.Agen_Fecha) = @Anio
			and MONTH(a.Agen_Fecha) = @Numero_Mes
			and DAY(a.Agen_Fecha) = @Dia
			and DATEPART(hh,a.Agen_Fecha) = @Hora
			and	DATEPART(mi,a.Agen_Fecha) = @Minuto

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

/*---------------------------------------OBTENCION IDs TURNO----------------------------------------------------------*/
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