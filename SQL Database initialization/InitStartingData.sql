-- Requiere que InitDatabase.sql ya se haya ejecutado (tablas creadas).
-- Es seguro re-ejecutarlo: todo el seed corre en UNA transaccion (todo o nada), y se
-- omite por completo solo si el usuario admin ya existe, lo que implica que el commit llego a ocurrir.
USE GestorTransporteCG;
GO

SET XACT_ABORT ON;

IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE Username = N'admin')
BEGIN
    BEGIN TRY
    BEGIN TRANSACTION;

    INSERT INTO dbo.Usuario (ID, Username, PasswordHash, Email, NumTelefono, EstaBloqueado, Idioma, IntentosFallidos, DVH)
    VALUES (
        'd1eda407-3582-4e0c-85cc-ae51eb67b826',
        'admin',
        '8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918',
        'admin@gmail.com',
        '+54 1120202020',
        0,
        DEFAULT,
        DEFAULT,
        '8D35CFBE2038902867920E5E76AFC58508CBDB31EB92820A1F1F444FF994A615'
    );

    INSERT INTO dbo.DVV (NombreTabla, ValorHash)
    VALUES (
        'Usuario',
        'EE3883C5E753048F5D8E02A1EED6B72FE04574293EFC6F894D103D8C94AAF0F2'
    );

    INSERT INTO dbo.Permiso (Nombre, EsPerfil) VALUES
    ('PERM-GESTIONAR-USR', 0),
    ('PERM-GESTIONAR-IDM', 0),
    ('PERM-DESBLOQUEAR-USR', 0),
    ('PERM-GESTIONAR-PERFIL', 0),
    ('PERM-CONSULTA-BIT', 0),
    ('PERM-GESTIONAR-HISTORIAL', 0),
    ('PERM-AGREGAR-IDM', 0),
    ('PERF-ADMIN', 1),
    ('PERM-PLANIFICACION-SERVICIO', 0),
    ('PERM-GESTION-CHOFERES-INTERNOS', 0),
    ('PERM-GESTION-CARGAS-COMBUSTIBLE', 0),
    ('PERM-GESTION-CRONOGRAMAS-MODIFICAR', 0),
    ('PERM-TALLER-AUDITAR', 0);

    INSERT INTO dbo.PermisoRelacion (ID_Padre, ID_Hijo) VALUES
    ((SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERF-ADMIN' AND EsPerfil = 1), (SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERM-GESTIONAR-USR' AND EsPerfil = 0)),
    ((SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERF-ADMIN' AND EsPerfil = 1), (SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERM-GESTIONAR-IDM' AND EsPerfil = 0)),
    ((SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERF-ADMIN' AND EsPerfil = 1), (SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERM-GESTIONAR-PERFIL' AND EsPerfil = 0)),
    ((SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERF-ADMIN' AND EsPerfil = 1), (SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERM-GESTIONAR-HISTORIAL' AND EsPerfil = 0)),
    ((SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERF-ADMIN' AND EsPerfil = 1), (SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERM-CONSULTA-BIT' AND EsPerfil = 0)),
    ((SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERF-ADMIN' AND EsPerfil = 1), (SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERM-AGREGAR-IDM' AND EsPerfil = 0));


    INSERT INTO dbo.PerfilUsuario (ID_Usuario, ID_Perfil) VALUES ('d1eda407-3582-4e0c-85cc-ae51eb67b826', (SELECT ID FROM dbo.Permiso WHERE Nombre = 'PERF-ADMIN' AND EsPerfil = 1));
    -------------

    -- ---------------------------------------------------------
    -- INSERTS INICIALES

    -- ---------------------------------------------------------
    -- 1. REGISTRAR LOS IDIOMAS
    -- ---------------------------------------------------------
    INSERT INTO dbo.Idioma (Codigo, Nombre) VALUES ('ES', N'Español');
    INSERT INTO dbo.Idioma (Codigo, Nombre) VALUES ('EN', N'English');
    INSERT INTO dbo.Idioma (Codigo, Nombre) VALUES ('PT', N'Português');

    -- ---------------------------------------------------------
    ------------------------------------------Separados por Idioma
-- =========================================================================
-- 1. TRADUCCIONES AL ESPAÑOL (ES)
-- =========================================================================
    INSERT INTO dbo.Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'MainUI', N'Sistema de gestion');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemCerrarSesion', N'Cerrar sesión');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemIniciarSesion', N'Iniciar sesión');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemGestionDeUsuarios', N'Gestión de usuarios');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemABMUsuarios', N'ABM Usuarios');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemDesbloqueoUsuarios', N'Desploqueo de usuarios');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemGestionDePerfiles', N'Gestión de perfiles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemABMPerfiles', N'Alta y asignación de perfiles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemInicio', N'Inicio');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemBitacora', N'Bitacora');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemConsultarBitacora', N'Consultar bitacora');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemPerfiles', N'Perfiles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemGestionarPerfiles', N'Gestionar perfiles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'label1', N'Idioma');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIGroupBoxAltaUsuario', N'Registrar usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIRegistroLabelUsername', N'Username');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIRegistroLabelEmail', N'Email');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIRegistroLabelNumTelefono', N'Número de telefono');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIRegistroLabelContrasena', N'Contraseña');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIRegistroLabelConfirmContrasena', N'Repetir Contraseña');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIButtonConfirmarRegistrarUsuario', N'Confirmar registro');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIGroupBoxListadoUsuarios', N'Listado de usuarios');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIGroupBoxModificacionUsuarios', N'Modificar usuario seleccionado');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIButtonConfirmarEliminarUsuario', N'Eliminar usuario seleccionado');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIModificacionLabelEmail', N'Email');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIModificacionLabelNumTelefono', N'Número de telefono');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionUsuariosUIModificacionButtonConfirmarModificar', N'Confirmar modificación');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'loginUILabelUsername', N'Usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'loginUILabelContrasena', N'Contraseña');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'loginUIButtonIniciarSesion', N'Iniciar sesión');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilesUIGroupBoxTreeView', N'Arbol de perfiles y permisos');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilesUILabelNombrePerfil', N'Nombre del nuevo perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilesUIButtonCrearPerfil', N'Crear perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilesUIGroupBoxListBoxPerfiles', N'Perfiles disponibles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilUIButtonAsignarPerfil', N'Asignar perfil a perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilesUIGroupBoxUsuarios', N'Usuarios disponibles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilUIButtonAsignarPerfilUsuario', N'Asignar perfil a usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilUIButtonDesasignarPerfilUsuario', N'Desasignar perfil a usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilesUIGroupBoxListBoxPermisos', N'Permisos disponibles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'perfilUIButtonAsignarPermiso', N'Asignar permiso a perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'bitacoraUILabelGrid', N'Registros de la bitacora');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'bitacoraUILabelComboBoxAccion', N'Filtrado por acción');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'bitacoraUILabelComboBoxUsername', N'Filtrado por username');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'bitacoraUIButtonLimpiarFiltros', N'Limpiar filtros');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'msg_InicioSesionExito', N'Inicio de sesión exitoso.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'msg_TituloExito', N'Éxito');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'msg_CierreSesionExito', N'Sesión cerrada correctamente.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'msg_TituloCierreSesion', N'Cerrar sesión');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_MaxIntentos', N'Ha superado los 3 intentos fallidos. Su cuenta ha sido bloqueada por seguridad.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_QuedanIntentos', N'Contraseña incorrecta. Le quedan {0} intentos antes de bloquearse.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_NoUserLogout', N'Usuario activo no encontrado en logout.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'log_InicioSesion', N'Inicio de Sesion');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'log_CierreSesion', N'Cierre de Sesion');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_UsuarioIncorrecto', N'Usuario incorrecto');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_UsuarioBloqueado', N'El usuario se encuentra bloqueado.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_EmailVacio', N'El correo electrónico no puede estar vacío');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_EmailFormato', N'El formato del correo electrónico no es válido');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_UsernameVacio', N'El nombre de usuario no puede estar vacio');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_UsernameFormato', N'El nombre de usuario debe tener entre 3 y 16 caracteres y solo puede contener letras, números, guiones bajos y guiones');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_PhoneVacio', N'El número de teléfono no puede estar vacío');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_PhoneFormato', N'El formato del número de teléfono no es válido');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_OnlyLettersVacio', N'El campo de texto no puede estar vacío');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_OnlyLettersFormato', N'El campo solo puede contener letras y espacios (se permiten acentos y eñes)');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_AlphaNumStrictVacio', N'El código o ID no puede estar vacío');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_AlphaNumStrictFormato', N'El campo solo puede contener letras (sin acentos) y números, sin espacios');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_AlphaNumSpacesVacio', N'El texto no puede estar vacío');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_AlphaNumSpacesFormato', N'El campo solo puede contener letras, números y espacios (sin caracteres especiales)');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_PassVacia', N'La contraseña no puede estar vacía.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_PassNoCoincide', N'Las contraseñas no coinciden.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_NoUserModificar', N'No se ha seleccionado ningún usuario para modificar.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_NoUserEliminar', N'No se ha seleccionado ningún usuario para eliminar.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'msg_TituloError', N'Error');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'btnDesbloquear', N'Desbloquear usuario seleccionado');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_NoUserDesbloquear', N'No se ha seleccionado ningún usuario para desbloquear.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'msg_DesbloqueoExito', N'Usuario desbloqueado correctamente.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'LOG_LOGIN', N'Inició sesión en el sistema');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'LOG_LOGOUT', N'Cerró sesión');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'LOG_USER_ADD', N'Registró a un nuevo usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'LOG_USER_MOD', N'Modificó los datos de un usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'LOG_USER_DEL', N'Eliminó a un usuario del sistema');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'LOG_PERFIL_ADD', N'Asignó un perfil a un usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'LOG_PERMISOS_MOD', N'Modificó permisos del sistema');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'GridBitacora_Usuario', N'Usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'GridBitacora_Fecha', N'Fecha y Hora');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'GridBitacora_Accion', N'Acción Realizada');

-- =========================================================================
-- 2. TRADUCCIONES AL INGLÉS (EN)
-- =========================================================================
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'MainUI', N'Management System');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemCerrarSesion', N'Logout');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemIniciarSesion', N'Login');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemGestionDeUsuarios', N'User Management');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemABMUsuarios', N'CRUD Users');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemDesbloqueoUsuarios', N'Unlock Users');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemGestionDePerfiles', N'Profile Management');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemABMPerfiles', N'Profile Creation & Assignment');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemInicio', N'Home');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemBitacora', N'Logbook');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemConsultarBitacora', N'View Logbook');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemPerfiles', N'Profiles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemGestionarPerfiles', N'Manage Profiles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'label1', N'Language');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIGroupBoxAltaUsuario', N'Register User');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIRegistroLabelUsername', N'Username');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIRegistroLabelEmail', N'Email');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIRegistroLabelNumTelefono', N'Phone Number');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIRegistroLabelContrasena', N'Password');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIRegistroLabelConfirmContrasena', N'Repeat Password');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIButtonConfirmarRegistrarUsuario', N'Confirm Registration');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIGroupBoxListadoUsuarios', N'User List');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIGroupBoxModificacionUsuarios', N'Modify Selected User');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIButtonConfirmarEliminarUsuario', N'Delete Selected User');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIModificacionLabelEmail', N'Email');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIModificacionLabelNumTelefono', N'Phone Number');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionUsuariosUIModificacionButtonConfirmarModificar', N'Confirm Modification');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'loginUILabelUsername', N'Username');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'loginUILabelContrasena', N'Password');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'loginUIButtonIniciarSesion', N'Login');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilesUIGroupBoxTreeView', N'Profiles and Permissions Tree');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilesUILabelNombrePerfil', N'New Profile Name');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilesUIButtonCrearPerfil', N'Create Profile');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilesUIGroupBoxListBoxPerfiles', N'Available Profiles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilUIButtonAsignarPerfil', N'Assign Profile to Profile');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilesUIGroupBoxUsuarios', N'Available Users');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilUIButtonAsignarPerfilUsuario', N'Assign Profile to User');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilUIButtonDesasignarPerfilUsuario', N'Unassign Profile from User');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilesUIGroupBoxListBoxPermisos', N'Available Permissions');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'perfilUIButtonAsignarPermiso', N'Assign Permission to Profile');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'bitacoraUILabelGrid', N'Binnacle entries');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'bitacoraUILabelComboBoxAccion', N'Filter by action');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'bitacoraUILabelComboBoxUsername', N'Filter by username');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'bitacoraUIButtonLimpiarFiltros', N'Clean filters');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'msg_InicioSesionExito', N'Successful login.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'msg_TituloExito', N'Success');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'msg_CierreSesionExito', N'Session closed successfully.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'msg_TituloCierreSesion', N'Logout');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_MaxIntentos', N'Maximum failed attempts exceeded. Your account has been locked for security.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_QuedanIntentos', N'Incorrect password. You have {0} attempts left before being locked.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_NoUserLogout', N'Active user not found on logout.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'log_InicioSesion', N'Login');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'log_CierreSesion', N'Logout');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_UsuarioIncorrecto', N'Incorrect user');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_UsuarioBloqueado', N'The user is locked.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_EmailVacio', N'Email cannot be empty');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_EmailFormato', N'Invalid email format');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_UsernameVacio', N'Username cannot be empty');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_UsernameFormato', N'Username must be between 3 and 16 characters and can only contain letters, numbers, underscores, and hyphens');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_PhoneVacio', N'Phone number cannot be empty');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_PhoneFormato', N'Invalid phone number format');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_OnlyLettersVacio', N'The text field cannot be empty');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_OnlyLettersFormato', N'The field can only contain letters and spaces (accents and ñ are allowed)');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_AlphaNumStrictVacio', N'Code or ID cannot be empty');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_AlphaNumStrictFormato', N'The field can only contain letters (no accents) and numbers, without spaces');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_AlphaNumSpacesVacio', N'Text cannot be empty');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_AlphaNumSpacesFormato', N'The field can only contain letters, numbers, and spaces (no special characters)');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_PassVacia', N'Password cannot be empty.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_PassNoCoincide', N'Passwords do not match.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_NoUserModificar', N'No user selected to modify.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_NoUserEliminar', N'No user selected to delete.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'msg_TituloError', N'Error');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'btnDesbloquear', N'Unlock selected user');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_NoUserDesbloquear', N'No user selected to unlock.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'msg_DesbloqueoExito', N'User unlocked successfully.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'LOG_LOGIN', N'Logged into the system');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'LOG_LOGOUT', N'Logged out');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'LOG_USER_ADD', N'Registered a new user');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'LOG_USER_MOD', N'Modified user details');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'LOG_USER_DEL', N'Deleted a user from the system');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'LOG_PERFIL_ADD', N'Assigned a profile to a user');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'LOG_PERMISOS_MOD', N'Modified system permissions');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'GridBitacora_Usuario', N'User');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'GridBitacora_Fecha', N'Date and Time');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'GridBitacora_Accion', N'Action Performed');

-- =========================================================================
-- 3. TRADUCCIONES AL PORTUGUÉS (PT)
-- =========================================================================
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'MainUI', N'Sistema de Gestão');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemCerrarSesion', N'Sair');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemIniciarSesion', N'Entrar');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemGestionDeUsuarios', N'Gestão de Usuários');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemABMUsuarios', N'CRUD de Usuários');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemDesbloqueoUsuarios', N'Desbloquear Usuários');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemGestionDePerfiles', N'Gestão de Perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemABMPerfiles', N'Criação e Atribuição de Perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemInicio', N'Início');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemBitacora', N'Livro de Bordo');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemConsultarBitacora', N'Visualizar Livro de Bordo');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemPerfiles', N'Perfis');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemGestionarPerfiles', N'Gerenciar Perfis');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'label1', N'Idioma');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIGroupBoxAltaUsuario', N'Registrar Usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIRegistroLabelUsername', N'Nome de usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIRegistroLabelEmail', N'E-mail');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIRegistroLabelNumTelefono', N'Número de Telefone');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIRegistroLabelContrasena', N'Senha');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIRegistroLabelConfirmContrasena', N'Repetir Senha');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIButtonConfirmarRegistrarUsuario', N'Confirmar Registro');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIGroupBoxListadoUsuarios', N'Lista de Usuários');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIGroupBoxModificacionUsuarios', N'Modificar Usuário Selecionado');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIButtonConfirmarEliminarUsuario', N'Excluir Usuário Selecionado');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIModificacionLabelEmail', N'E-mail');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIModificacionLabelNumTelefono', N'Número de Telefone');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionUsuariosUIModificacionButtonConfirmarModificar', N'Confirmar Modificação');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'loginUILabelUsername', N'Nome de usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'loginUILabelContrasena', N'Senha');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'loginUIButtonIniciarSesion', N'Entrar');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilesUIGroupBoxTreeView', N'Árvore de Perfis e Permissões');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilesUILabelNombrePerfil', N'Nome do Novo Perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilesUIButtonCrearPerfil', N'Criar Perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilesUIGroupBoxListBoxPerfiles', N'Perfis Disponíveis');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilUIButtonAsignarPerfil', N'Atribuir Perfil a Perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilesUIGroupBoxUsuarios', N'Usuários Disponíveis');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilUIButtonAsignarPerfilUsuario', N'Atribuir Perfil a Usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilUIButtonDesasignarPerfilUsuario', N'Remover Perfil do Usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilesUIGroupBoxListBoxPermisos', N'Permissões Disponíveis');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'perfilUIButtonAsignarPermiso', N'Atribuir Permissão ao Perfil');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'bitacoraUILabelGrid', N'Registros de Borda');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'bitacoraUILabelComboBoxAccion', N'Filtrar por ação');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'bitacoraUILabelComboBoxUsername', N'Filtrar por nome de usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'bitacoraUIButtonLimpiarFiltros', N'Limpar filtros');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'msg_InicioSesionExito', N'Login bem-sucedido.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'msg_TituloExito', N'Sucesso');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'msg_CierreSesionExito', N'Sessão encerrada com sucesso.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'msg_TituloCierreSesion', N'Sair');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_MaxIntentos', N'Limite de tentativas excedido. Sua conta foi bloqueada por segurança.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_QuedanIntentos', N'Senha incorreta. Você tem {0} tentativas restantes antes de ser bloqueado.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_NoUserLogout', N'Usuário ativo não encontrado no logout.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'log_InicioSesion', N'Início de Sessão');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'log_CierreSesion', N'Encerramento de Sessão');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_UsuarioIncorrecto', N'Usuário incorreto');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_UsuarioBloqueado', N'O usuário está bloqueado.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_EmailVacio', N'O e-mail não pode estar vazio');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_EmailFormato', N'Formato de e-mail inválido');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_UsernameVacio', N'O nome de usuário não pode estar vazio');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_UsernameFormato', N'O nome de usuário deve ter entre 3 e 16 caracteres e só pode conter letras, números, sublinhados e hifens');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_PhoneVacio', N'O número de telefone não pode estar vazio');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_PhoneFormato', N'Formato de número de telefone inválido');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_OnlyLettersVacio', N'O campo de texto não pode estar vazio');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_OnlyLettersFormato', N'O campo só pode conter letras e espaços (acentos e cedilhas são permitidos)');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_AlphaNumStrictVacio', N'O código ou ID não pode estar vazio');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_AlphaNumStrictFormato', N'O campo só pode conter letras (sem acentos) e números, sem espaços');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_AlphaNumSpacesVacio', N'O texto não pode estar vazio');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_AlphaNumSpacesFormato', N'O campo só pode conter letras, números e espaços (sem caracteres especiais)');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_PassVacia', N'A senha não pode estar vazia.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_PassNoCoincide', N'As senhas não coincidem.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_NoUserModificar', N'Nenhum usuário selecionado para modificar.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_NoUserEliminar', N'Nenhum usuário selecionado para excluir.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'msg_TituloError', N'Erro');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'btnDesbloquear', N'Desbloquear usuário selecionado');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_NoUserDesbloquear', N'Nenhum usuário selecionado para desbloquear.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'msg_DesbloqueoExito', N'Usuário desbloqueado com sucesso.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'LOG_LOGIN', N'Entrou no sistema');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'LOG_LOGOUT', N'Saiu do sistema');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'LOG_USER_ADD', N'Registrou um novo usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'LOG_USER_MOD', N'Modificou os dados de um usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'LOG_USER_DEL', N'Excluiu um usuário do sistema');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'LOG_PERFIL_ADD', N'Atribuiu um perfil a um usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'LOG_PERMISOS_MOD', N'Modificou as permissões do sistema');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'GridBitacora_Usuario', N'Usuário');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'GridBitacora_Fecha', N'Data e Hora');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'GridBitacora_Accion', N'Ação Realizada');

-- Traducciones para la etiqueta: gestionHistorialUILabelGridUsuarios
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionHistorialUILabelGridUsuarios', N'Usuarios disponibles');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionHistorialUILabelGridUsuarios', N'Available users');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionHistorialUILabelGridUsuarios', N'Usuários disponíveis');

-- Traducciones para la etiqueta: gestionHistorialUILabelGridEstadoUsuarios
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionHistorialUILabelGridEstadoUsuarios', N'Historial del usuario seleccionado');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionHistorialUILabelGridEstadoUsuarios', N'Selected user history');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionHistorialUILabelGridEstadoUsuarios', N'Histórico do usuário selecionado');

-- Traducciones para la etiqueta: mainUIStripMenuItemHistorialUsuario
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'mainUIStripMenuItemHistorialUsuario', N'Historial usuario');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'mainUIStripMenuItemHistorialUsuario', N'User history');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'mainUIStripMenuItemHistorialUsuario', N'Histórico do usuário');

-- Traducciones para el botón: gestionHistorialUIButtonRecuperarEstado
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'gestionHistorialUIButtonRecuperarEstado', N'Recuperar estado');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'gestionHistorialUIButtonRecuperarEstado', N'Restore state');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'gestionHistorialUIButtonRecuperarEstado', N'Restaurar estado');

-- =========================================================================
-- TRADUCCIONES DEL NUEVO MÓDULO: GESTIÓN DE IDIOMAS
-- =========================================================================

-- 1. Botón del Menú Principal (MainUI)
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'agregarIdiomaToolStripMenuItem', N'Agregar Idioma');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'agregarIdiomaToolStripMenuItem', N'Add Language');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'agregarIdiomaToolStripMenuItem', N'Adicionar Idioma');

-- 2. Título del Formulario (GestionIdiomasUI)
-- Nota: La Key coincide con la propiedad "Name" del Formulario.
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'GestionIdiomasUI', N'Configuración de Nuevos Idiomas');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'GestionIdiomasUI', N'New Languages Configuration');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'GestionIdiomasUI', N'Configuração de Novos Idiomas');

-- 3. Etiquetas (Labels) y Botones del Formulario
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'labelCodigo', N'Código (Ej: FR):');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'labelCodigo', N'Code (e.g., FR):');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'labelCodigo', N'Código (Ex: FR):');

INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'labelNombre', N'Nombre Idioma:');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'labelNombre', N'Language Name:');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'labelNombre', N'Nome do Idioma:');

INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'btnGuardarIdioma', N'Guardar Idioma');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'btnGuardarIdioma', N'Save Language');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'btnGuardarIdioma', N'Salvar Idioma');

-- 4. Cabeceras del DataGridView (Asignadas dinámicamente)
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'GridIdioma_ColKey', N'Componente / Etiqueta');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'GridIdioma_ColKey', N'Component / Label');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'GridIdioma_ColKey', N'Componente / Rótulo');

INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'GridIdioma_ColRef', N'Referencia (Español)');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'GridIdioma_ColRef', N'Reference (Spanish)');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'GridIdioma_ColRef', N'Referência (Espanhol)');

INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'GridIdioma_ColNuevo', N'Nueva Traducción');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'GridIdioma_ColNuevo', N'New Translation');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'GridIdioma_ColNuevo', N'Nova Tradução');

-- 5. Mensajes de Éxito, Validaciones y Errores (MessageBox / Exceptions)
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'msg_IdiomaGuardadoExito', N'El idioma y sus respectivas traducciones se han guardado exitosamente.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'msg_IdiomaGuardadoExito', N'The language and its translations have been saved successfully.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'msg_IdiomaGuardadoExito', N'O idioma e suas traduções foram salvos com sucesso.');

INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_CodigoNombreObligatorios', N'El código y el nombre del idioma son obligatorios.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_CodigoNombreObligatorios', N'Language code and name are required.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_CodigoNombreObligatorios', N'O código e o nome do idioma são obrigatórios.');

INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_IdiomaYaExiste', N'El código de idioma ya se encuentra registrado en el sistema.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_IdiomaYaExiste', N'The language code is already registered in the system.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_IdiomaYaExiste', N'O código do idioma já está registrado no sistema.');

INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_TraduccionObligatoria', N'Debe proveer al menos una traducción para el nuevo idioma.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_TraduccionObligatoria', N'You must provide at least one translation for the new language.');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_TraduccionObligatoria', N'Você deve fornecer pelo menos uma tradução para o novo idioma.');

INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('ES', 'err_CargarEtiquetas', N'Error al cargar etiquetas de referencia: ');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('EN', 'err_CargarEtiquetas', N'Error loading reference labels: ');
INSERT INTO Traduccion (CodigoIdioma, KeyEtiqueta, Texto) VALUES ('PT', 'err_CargarEtiquetas', N'Erro ao carregar rótulos de referência: ');

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
