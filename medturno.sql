-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 31-05-2022 a las 19:26:44
-- Versión del servidor: 10.4.17-MariaDB
-- Versión de PHP: 8.0.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `medturno`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `direccion`
--

CREATE TABLE `direccion` (
  `id` int(11) NOT NULL,
  `calle` varchar(150) COLLATE utf8_spanish_ci NOT NULL,
  `numero` int(11) NOT NULL,
  `ciudad` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `estado` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `direccion`
--

INSERT INTO `direccion` (`id`, `calle`, `numero`, `ciudad`, `estado`) VALUES
(1, 'los lapachos', 33, 'San luis', 1),
(2, 'Crnel Mercau', 334, 'San luis', 1),
(3, 'Pte Peron', 337, 'Juana Koslay', 1),
(4, 'Los Incas', 433, 'Cortaderas', 1),
(5, 'los Almendros', 383, 'San luis', 1),
(6, 'Mitre', 393, 'Merlo', 1),
(7, 'Ayacucho', 633, 'San luis', 1),
(8, 'los caldenes', 1233, 'San luis', 1),
(9, 'Legislatura', 22, 'san luis', 1),
(10, 'acanomas', 34, 'san luis', 1),
(11, 'sin direccion', 0, 'sin direccion', 0),
(12, 'Centenario', 96, 'San Luis', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `doctor`
--

CREATE TABLE `doctor` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `matricula` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `horarioatencion` varchar(200) COLLATE utf8_spanish_ci NOT NULL,
  `idEspecialidad` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `doctor`
--

INSERT INTO `doctor` (`id`, `nombre`, `matricula`, `horarioatencion`, `idEspecialidad`, `estado`) VALUES
(1, 'Dr. Arrua Andrea', 'M.P. 3456', 'Lun: 10 - 13hs. Mier: 14 - 16hs. Vier: 08 - 11hs', 1, 1),
(2, 'Dr. Lemos Gerardo', 'M.P 936', 'Mar: 08 - 13hs. Mier: 08 - 12.30hs. Jue: 18 - 21hs', 2, 1),
(3, 'Dra. Aime Paula', 'M.P. 034', 'Lun: 20 - 22hs', 3, 1),
(4, 'Dra. Moran Leticia', 'M.P. 2367', 'Lun a Vie: 15 - 21hs', 5, 1),
(5, 'Dr. Tamashiro Ohiro', 'M.P. 19453', 'Lun: 14 - 18hs. Mar: 14 - 18hs. jue: 17 - 20hs', 4, 1),
(6, 'Dr. Molina Campos', 'M.P. 0001', 'Lun a Vie: 09.30 - 13hs', 6, 1),
(7, 'Lic. Calderon Andrada', 'M.P. 4545', 'Jue: 14 - 18hs', 7, 1),
(8, 'Dra. Moran Viviana', 'M.N. 64899', 'Jue: 14 - 21hs', 6, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `especialidad`
--

CREATE TABLE `especialidad` (
  `id` int(11) NOT NULL,
  `tipo` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `especialidad` varchar(100) COLLATE utf8_spanish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `especialidad`
--

INSERT INTO `especialidad` (`id`, `tipo`, `especialidad`) VALUES
(1, 'Odontologia', 'Ortodoncia'),
(2, 'Cardiologo', 'Coronario'),
(3, 'Medicina General', 'Medicina Familiar'),
(4, 'Traumatologia', 'Ortopedia'),
(5, 'Medicina General', 'Cronicos'),
(6, 'Psicologia', 'Atencio infantil'),
(7, 'Ginecología', 'Tocoginecología'),
(8, 'Odontologia', 'Ortodoncia');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `prestador`
--

CREATE TABLE `prestador` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `direccion` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `telefono` int(50) NOT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `prestador`
--

INSERT INTO `prestador` (`id`, `nombre`, `direccion`, `telefono`, `estado`) VALUES
(1, 'OBRA SOCIAL DEL PERSONAL DE LA ACTIVIDAD AZUCARERA TUCUMANA', 'CONGRESO DE TUCUMAN 341 | SAN MIGUEL DE TUCUMAN', 800123456, 1),
(2, 'Sin Prestador', 'Sin Prestador', 0, 0),
(7, 'OBRA SOCIAL DEL PERSONAL DE LA ACTIVIDAD AZUCARERA TUCUMANA', 'CONGRESO DE TUCUMAN 341 | SAN MIGUEL DE TUCUMAN', 800123456, 1),
(8, 'OBRA SOCIAL DEL PERSONAL DE PANADERIAS Y AFINES', 'BOEDO 168 | CAPITAL FEDERAL - BOEDO(1-400)', 800123456, 1),
(9, 'OBRA SOCIAL DE TRABAJADORES DE LA INDUSTRIA DEL GAS', 'BOEDO 90 | CAPITAL FEDERAL - BOEDO(1-400)', 800123456, 1),
(10, 'OBRA SOCIAL CONDUCTORES DE TAXIS DE CORDOBA', 'SAN LUIS 373 - B. GUEMES | CORDOBA', 800123456, 1),
(11, 'OBRA SOCIAL DEL PERSONAL DE TELEVISION Y AFINES', 'QUINTINO BOCAYUVA 38/50 | BOCAYUVA QUINTINO(1-500)', 800123456, 1),
(12, 'OBRA SOCIAL DE LAS ASOCIACIONES DE EMPLEADOS DE FARMACIA', 'RINCON 1035 | CAPITAL FEDERAL - RINCON(501-1200)', 800123456, 1),
(13, 'OBRA SOCIAL DEL PERSONAL DEL ESPECTACULO PUBLICO', 'PASCO 148/54 | CAPITAL FEDERAL - PASCO(1-500)', 800123456, 1),
(14, 'OBRA SOCIAL DE CONDUCTORES NAVALES MILITARES', 'PINZON 281 | CAPITAL FEDERAL - PINZON(1-1200)', 800123456, 1),
(15, 'OBRA SOCIAL DE LOCUTORES', 'VIDT 2011 | CAPITAL FEDERAL - VIDT(1601-2200)', 800123456, 1),
(16, 'OBRA SOCIAL DEL PERSONAL DE MAESTRANZA', 'ALZAGA 2271 | CAPITAL FEDERAL - ALZAGA(2001-2300)', 800123456, 1),
(17, 'OBRA SOCIAL DEL PERSONAL DE LA INDUSTRIA DEL CAUCHO', 'CONGRESO 2033 | CAPITAL FEDERAL - CONGRESO(1501-3300)', 800123456, 1),
(18, 'OBRA SOCIAL DE LUZ Y FUERZA DE LA PATAGONIA', 'BALDOMERO CARRASCO Nº 60 | TRELEW', 800123456, 1),
(19, 'OBRA SOCIAL DEL PERSONAL DE LA INDUSTRIA BOTONERA', 'CALLE 91 N# 1886 | SAN MARTIN ,PTDO. GENERAL SAN', 800123456, 1),
(20, 'DOSEP - DIRECCION OBRA SOCIAL DEL EMPLEADO PUBLICO', 'Ayacucho y Chacabuco', 4545678, 1),
(26, 'O.S. CAMIONEROS DEL SUR', 'CONGRESO 341 | USHUAIA', 455667, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `turnos`
--

CREATE TABLE `turnos` (
  `id` int(11) NOT NULL,
  `fechaSolicitud` varchar(11) COLLATE utf8_spanish_ci NOT NULL DEFAULT current_timestamp(),
  `start` varchar(50) COLLATE utf8_spanish_ci NOT NULL,
  `end` varchar(50) COLLATE utf8_spanish_ci NOT NULL,
  `color` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `descripcion` varchar(200) COLLATE utf8_spanish_ci NOT NULL,
  `textColor` varchar(100) COLLATE utf8_spanish_ci NOT NULL DEFAULT '#FFFFFF',
  `title` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `idPrestador` int(11) NOT NULL,
  `idDoctor` int(11) NOT NULL,
  `idUsuario` int(11) NOT NULL,
  `estado` int(11) NOT NULL DEFAULT 2 COMMENT '0:eliminado, 1:particular, 2:pendiente, 3:rechazado, 4:atendido, 5:urgente, 6:prioritario, 7:OSocial, 8:normal'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `turnos`
--

INSERT INTO `turnos` (`id`, `fechaSolicitud`, `start`, `end`, `color`, `descripcion`, `textColor`, `title`, `idPrestador`, `idDoctor`, `idUsuario`, `estado`) VALUES
(1, '2022-03-12 ', '2022-03-03 14:10', '2022-03-03 14:10', '#f5af19', 'Recetas', '#FFFFFF', 'Su Turno: 1 - Control', 14, 1, 2, 3),
(2, '2022-03-12 ', '2022-03-04 18:30:00 ', '2022-03-04 18:30:00 ', '#c31432', 'trae RX', '#FFFFFF', 'Su Turno: 1 - Control de Yeso', 17, 5, 6, 0),
(3, '2022-03-12 ', '2022-03-07 17:20', '2022-03-07 17:20', '#52af52', 'Bracket', '#FFFFFF', 'Su Turno: 1 - Control', 19, 1, 9, 8),
(4, '2022-03-12 ', '2022-03-07 17:30', '2022-03-07 17:30', '#f5af19', 'Dolor Muela', '#FFFFFF', 'Su Turno: 2 - Arreglo', 13, 1, 8, 3),
(5, '2022-03-12 ', '2022-03-09 11:05', '2022-03-09 11:05', '#52af52', 'fuerte caida de escalera', '#FFFFFF', 'Su Turno: 1 - Caida', 9, 6, 7, 5),
(6, '2022-03-12 ', '2022-03-10 09:00', '2022-03-10 09:00', '#ffff0070', 'Dolor lumbar', '#FFFFFF', 'Su Turno: 1 - Dolor', 17, 4, 6, 6),
(7, '2022-03-12 ', '2022-03-16 16:45', '2022-03-16 16:45', '#52af52', 'Recetas medicamentos', '#FFFFFF', 'Su Turno: 1 - Recetas', 10, 3, 3, 8),
(8, '2022-03-12 ', '2022-03-27 20:40', '2022-03-27 20:40', '#52af52', 'Ve nublado ojo izquierdo', '#FFFFFF', 'Su Turno: 1 - Ojos', 13, 2, 8, 8),
(9, '2022-03-12 ', '2022-03-12 17:05', '2022-03-12 17:05', '#ff0000', 'Mareo vomito diarrea', '#FFFFFF', 'Su Turno: 1 - Mareos', 17, 6, 6, 5),
(10, '2022-03-12 ', '2022-03-12 17:10', '2022-03-12 17:10', '#52af52', 'orina con sangrado', '#FFFFFF', 'Su Turno: 2 - Sangrado', 19, 6, 9, 8),
(11, '2022-03-12 ', '2022-03-24 12:00', '2022-03-24 12:00', '#ffff0070', 'con dilatacion', '#FFFFFF', 'Su Turno: 1 - Contracciones', 17, 3, 6, 6),
(12, '2022-03-12 ', '2022-03-16 16:55', '2022-03-16 16:55', '#52af52', 'Recetas corticoides', '#FFFFFF', 'Su Turno: 2 - Control', 10, 3, 3, 8),
(13, '2022-03-15 ', '2022-03-15 18:30', '2022-03-15 18:30', '#52af52', 'trae resultados de orina', '#FFFFFF', 'Su Turno: 1 - Analisis', 9, 3, 7, 8),
(14, '2022-03-21 ', '2022-03-23 05:50', '2022-03-23 05:50', '#ffff0070', 'Muela de juicio', '#FFFFFF', 'Su Turno: 1 - duelor muela de juicio feo', 10, 1, 3, 6),
(15, '2022-03-23 ', '2022-03-25 18:30', '2022-03-25 18:30', '#e67e7e', 'Certificado por faltas', '#FFFFFF', 'Su Turno: 1 - Certificado', 16, 3, 11, 7),
(16, '2022-03-24 ', '2022-03-31 16:20', '2022-03-31 16:20', '#c31432', 'colicos', '#FFFFFF', 'Su Turno: 1 - Digestion lenta', 9, 2, 7, 0),
(17, '2022-05-30 ', '2022-06-02 18:30', '2022-06-02 18:30', '#c31432', 'dolor', '#FFFFFF', 'Su Turno: 1 - Caries', 20, 1, 12, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `dni` int(50) NOT NULL,
  `fecNac` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `mail` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `telefono` int(50) NOT NULL,
  `avatar` varchar(50) COLLATE utf8_spanish_ci NOT NULL,
  `password` varchar(250) COLLATE utf8_spanish_ci NOT NULL,
  `pregunta` varchar(100) COLLATE utf8_spanish_ci NOT NULL,
  `rol` int(11) NOT NULL COMMENT '1:Admin, 2:Empleado, 3:paciente, 4:doctor',
  `fecAlta` datetime NOT NULL DEFAULT current_timestamp(),
  `estado` tinyint(1) NOT NULL DEFAULT 1,
  `idprestador` int(11) NOT NULL,
  `idDireccion` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id`, `nombre`, `dni`, `fecNac`, `mail`, `telefono`, `avatar`, `password`, `pregunta`, `rol`, `fecAlta`, `estado`, `idprestador`, `idDireccion`) VALUES
(1, 'hashLog', 0, '0000-00-00 00:00:00', 'hashLog@mail.com', 0, '../..', '', 'hash', 0, '1700-03-22 17:09:18', 0, 2, 11),
(2, 'Super Admin', 222222, '1980-02-13', 'admin@mail.com', 2645656, '/img/avatars/admin.jpg', 'Od5l8kFX8vwvOF5BPAZjFy6qbs/5m9WgQzPkKL1TqA8=', 'admin', 1, '2022-03-09 21:44:29', 1, 2, 11),
(3, 'Alvaro Dante', 456789, '1970-02-13', 'dante@mail.com', 2645656, '/img/avatars/person.png', '1YTD46gt2Z3a7nHRAD1xOuveWcFwr6S74ase1jkD1Fw=', 'dante', 3, '2022-03-09 21:50:11', 1, 10, 2),
(4, 'Alvarez Laura', 477484, '1970-08-02', 'alvarez@mail.com', 264565, '/img/avatars/Empleado.png', 'Od5l8kFX8vwvOF5BPAZjFy6qbs/5m9WgQzPkKL1TqA8=', 'admin', 2, '2022-03-09 22:10:06', 1, 7, 3),
(5, 'Rios Roxana', 238343, '1970-08-02', 'rios@mail.com', 2645656, '/img/avatars/Empleado.png', 'Od5l8kFX8vwvOF5BPAZjFy6qbs/5m9WgQzPkKL1TqA8=', 'admin', 2, '2022-03-09 22:10:06', 1, 12, 4),
(6, 'Nadal Nuria', 923746, '1999-08-02', 'nadal@mail.com', 2645656, '/img/avatars/person.png', 'Od5l8kFX8vwvOF5BPAZjFy6qbs/5m9WgQzPkKL1TqA8=', 'admin', 3, '2022-03-09 22:10:06', 1, 17, 5),
(7, 'Zelada Sara', 4137422, '1987-08-02 00:00:00.000000', 'zelada@mail.com', 2645656, '/img/avatars/person.png', 'Od5l8kFX8vwvOF5BPAZjFy6qbs/5m9WgQzPkKL1TqA8=', 'admin', 3, '2022-03-09 22:10:06', 1, 9, 6),
(8, 'Cuello Exequiel', 28346, '1995-08-02', 'cuello@mail.com', 2645656, '/img/avatars/person.png', 'Od5l8kFX8vwvOF5BPAZjFy6qbs/5m9WgQzPkKL1TqA8=', 'admin', 3, '2022-03-09 22:10:06', 1, 13, 7),
(9, 'Bustos Marina', 287499, '1979-08-02', 'bustoa@mail.com', 2645656, '/img/avatars/person.png', 'Od5l8kFX8vwvOF5BPAZjFy6qbs/5m9WgQzPkKL1TqA8=', 'admin', 3, '2022-03-09 22:10:06', 1, 19, 8),
(10, 'Varas Pamela', 27777777, '2022-03-16 00:00:00', 'varas@mail.com', 343434, '/img/avatars/person.png', 'P1UvNMCPWaLd9qWd7h9NJ7OiwxU/aIuRBFuCvooTcu8=', 'varas', 3, '2022-03-15 23:12:10', 1, 15, 9),
(11, 'Muñoz Euge', 343434, '2022-03-08 00:00:00', 'euge@mail.com', 34343434, '/img/avatars/person.png', 'lO6UGKp8xfFllZf/csDS3NGaCD55HOwKjTr/gp9KW3M=', 'euge', 3, '2022-03-20 22:57:27', 1, 16, 10),
(12, 'Fernandez Hernan', 3567890, '2022-03-08 00:00:00', 'fhernan@mail.com', 433553, '/img/avatars/person.png', '1t7FbYPIp3sxG0SnP1bDMGrbsUo+TW9K2B62iiHi1Yw=', 'hernan', 3, '2022-03-28 21:09:04', 1, 20, 12);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `direccion`
--
ALTER TABLE `direccion`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `doctor`
--
ALTER TABLE `doctor`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idEspecialidad` (`idEspecialidad`);

--
-- Indices de la tabla `especialidad`
--
ALTER TABLE `especialidad`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `prestador`
--
ALTER TABLE `prestador`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `turnos`
--
ALTER TABLE `turnos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idPrestador` (`idPrestador`,`idDoctor`,`idUsuario`),
  ADD KEY `idUsuario` (`idUsuario`),
  ADD KEY `idDoctor` (`idDoctor`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idprestador` (`idprestador`),
  ADD KEY `idDireccion` (`idDireccion`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `direccion`
--
ALTER TABLE `direccion`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `doctor`
--
ALTER TABLE `doctor`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `especialidad`
--
ALTER TABLE `especialidad`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `prestador`
--
ALTER TABLE `prestador`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT de la tabla `turnos`
--
ALTER TABLE `turnos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `doctor`
--
ALTER TABLE `doctor`
  ADD CONSTRAINT `doctor_ibfk_1` FOREIGN KEY (`idEspecialidad`) REFERENCES `especialidad` (`id`);

--
-- Filtros para la tabla `turnos`
--
ALTER TABLE `turnos`
  ADD CONSTRAINT `turnos_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`id`),
  ADD CONSTRAINT `turnos_ibfk_3` FOREIGN KEY (`idPrestador`) REFERENCES `prestador` (`id`),
  ADD CONSTRAINT `turnos_ibfk_4` FOREIGN KEY (`idDoctor`) REFERENCES `doctor` (`id`);

--
-- Filtros para la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `usuario_ibfk_1` FOREIGN KEY (`idprestador`) REFERENCES `prestador` (`id`),
  ADD CONSTRAINT `usuario_ibfk_2` FOREIGN KEY (`idDireccion`) REFERENCES `direccion` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
