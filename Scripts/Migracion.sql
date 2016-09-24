DELETE FROM CHAMBA.Bonos
DELETE FROM CHAMBA.Compra_Bonos
DBCC CHECKIDENT ('CHAMBA.Compra_Bonos', RESEED, 0)
DELETE FROM CHAMBA.Consultas
DELETE FROM CHAMBA.Turnos
DELETE FROM CHAMBA.Tipo_Especialidad_X_Profesional
DELETE FROM CHAMBA.Profesionales
DELETE FROM CHAMBA.Pacientes
DELETE FROM CHAMBA.Usuarios
DELETE FROM CHAMBA.Planes
DELETE FROM CHAMBA.Tipo_Especialidad
DELETE FROM CHAMBA.Especialidades
DELETE FROM CHAMBA.Roles

/* CREACION DE ROLES */
INSERT INTO CHAMBA.Roles (Rol_Id, Rol_Nombre, Rol_Estado) VALUES (1, 'Administrador', 1), (2, 'Profesional', 1), (3, 'Paciente', 1)

/* MIGRACION DE ESPECIALIDADES */

INSERT INTO CHAMBA.Especialidades (Espe_Codigo, Espe_Descripcion)
SELECT DISTINCT Especialidad_Codigo, Especialidad_Descripcion 
FROM gd_esquema.Maestra
WHERE Especialidad_Codigo IS NOT NULL

/* MIGRACION DE TIPOS DE ESPECIALIDAD */

DECLARE @Temp numeric(18,0)

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


/* DECLARACION DE CURSOR DE PACIENTES */

DECLARE cursorPacientes CURSOR FOR SELECT DISTINCT(Paciente_DNI), Paciente_Nombre, Paciente_Apellido, Paciente_Direccion, Paciente_Telefono, Paciente_Mail, Paciente_Fecha_Nac, Plan_Med_Codigo
FROM gd_esquema.Maestra
WHERE Paciente_DNI IS NOT NULL


/* DECLARACION DE VARIABLES PARA CURSORES */

DECLARE @DNI numeric(18,0), @Nombre varchar(255), @Apellido varchar(255), @Direccion varchar(255), @Telefono numeric(18,0), @Mail varchar(255), @Fecha_Nac datetime
DECLARE @Plan numeric(18,0)
DECLARE @Existe INT

/* MIGRACION DE PACIENTES */

OPEN cursorPacientes
FETCH NEXT FROM cursorPacientes INTO @DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac, @Plan
WHILE @@FETCH_STATUS=0
BEGIN

SELECT @Existe = COUNT(*) FROM CHAMBA.Usuarios WHERE Usua_DNI = @DNI

IF (@Existe = 0)
	INSERT INTO CHAMBA.Usuarios (Usua_DNI, Usua_TipoDNI, Usua_Nombre, Usua_Apellido, Usua_Direccion, Usua_Telefono, Usua_Mail, Usua_Fecha_Nac, Usua_Sexo)
	VALUES (@DNI, 1, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac, 'M')

INSERT INTO CHAMBA.Pacientes (Paci_Usuario, Paci_Plan) VALUES (@DNI, @Plan)

FETCH NEXT FROM cursorPacientes INTO @DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac, @Plan
END
CLOSE cursorPacientes
DEALLOCATE cursorPacientes

/* DECLARACION DE CURSOR DE PROFESIONALES */

DECLARE cursorMedicos CURSOR FOR SELECT DISTINCT(Medico_DNI), Medico_Nombre, Medico_Apellido, Medico_Direccion, Medico_Telefono, Medico_Mail, Medico_Fecha_Nac
FROM gd_esquema.Maestra
WHERE Medico_DNI IS NOT NULL


/* MIGRACION DE PROFESIONALES */

OPEN cursorMedicos
FETCH NEXT FROM cursorMedicos INTO @DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac
WHILE @@FETCH_STATUS=0
BEGIN

SELECT @Existe = COUNT(*) FROM CHAMBA.Usuarios WHERE Usua_DNI = @DNI

IF (@Existe = 0)
	INSERT INTO CHAMBA.Usuarios (Usua_DNI, Usua_TipoDNI, Usua_Nombre, Usua_Apellido, Usua_Direccion, Usua_Telefono, Usua_Mail, Usua_Fecha_Nac, Usua_Sexo)
	VALUES (@DNI, 1, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac, 'M')

INSERT INTO CHAMBA.Profesionales (Prof_Usuario) VALUES (@DNI)


FETCH NEXT FROM cursorMedicos INTO @DNI, @Nombre, @Apellido, @Direccion, @Telefono, @Mail, @Fecha_Nac
END
CLOSE cursorMedicos
DEALLOCATE cursorMedicos


/* MIGRACION DE TIPO_ESPECIALIDAD_X_PROFESIONAL */

INSERT INTO CHAMBA.Tipo_Especialidad_X_Profesional (Tipo_Espec_X_Pof_Tipo_Especialidad, Tipo_Espec_X_Prof_Profesional)
SELECT CAST(CAST(Especialidad_Codigo AS VARCHAR(18)) + CAST(Tipo_Especialidad_Codigo AS VARCHAR(18)) AS NUMERIC(18,0)), Medico_DNI FROM
(SELECT DISTINCT Especialidad_Codigo, Tipo_Especialidad_Codigo, Medico_DNI
FROM gd_esquema.Maestra
WHERE Tipo_Especialidad_Codigo IS NOT NULL) AS S1


/* MIGRACION DE TURNOS */

INSERT INTO CHAMBA.Turnos (Turn_Numero, Turn_Fecha, Turn_Paciente, Turn_Profesional)
SELECT DISTINCT Turno_Numero, Turno_Fecha, Paciente_Dni, Medico_DNI
FROM gd_esquema.Maestra
WHERE Turno_Numero IS NOT NULL


/* MIGRACION DE CONSULTAS */

INSERT INTO CHAMBA.Consultas (Cons_Turno, Cons_Sintoma, Cons_Diagnostico)
SELECT Turno_Numero, Consulta_Sintomas, Consulta_Enfermedades
FROM gd_esquema.Maestra
WHERE Consulta_Sintomas IS NOT NULL


/* MIGRACION DE COMPRA DE BONOS */

INSERT INTO CHAMBA.Compra_Bonos(Comp_Bono_Fecha, Comp_Bono_Paciente, Comp_Bono_Plan)
SELECT DISTINCT Compra_Bono_Fecha, Paciente_DNI, Plan_Med_Codigo
FROM gd_esquema.Maestra
WHERE Compra_Bono_Fecha IS NOT NULL