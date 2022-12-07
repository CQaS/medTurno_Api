<!doctype html>
<html lang="es">
  <head>
    <title>Calendario - medTurno</title>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Bootstrap CSS v5.0.2 -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css"  integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="js/jquery.min.js"></script>
    <script src="js/moment.min.js"></script>
    <link rel="stylesheet" href="css//fullcalendar.min.css">
    <script src="js/fullcalendar.min.js"></script>
    <script src="js/es.js"></script>
    <link rel="stylesheet" href="css/calendarStyle.css">
    <script src="js/bootstrap-clockpicker.js"></script>
    <link rel="stylesheet" href="css/bootstrap-clockpicker.css">

    <!-- limpia cache ctr+f5  -->
  </head>
  <body>

    <img class="logomedturno" src="img/medturno.jpg" alt="medTurno">

    <div class="login">      
      <h2><?php echo $init['password']; ?></h2>
    </div>

    <div class="btnadmin">
      <a href="http://localhost:5000/Home/Admin" class="btn btn-primary">Home - Admin</a>
    </div>
    <h4 id="tur">
      <span>med</span>
      <span>T</span>
      <span>urno</span>
    </h4><br>
    <center><h3>CALENDARIO de Turnos</h3></center>
  
      <div class="container">
        
          <div class="row">
                <div class="col"></div>
                <div class="col7">
                  <br>
                      <div>
                        <select id="listmed" onchange="selectMed();" style="background: #0563af;color: white;padding: 10px;width: 230px;height: 50px;border: none;font-size: 20px;box-shadow: 0 5px 25px rgba(0, 0, 0, .5);cursor: pointer;border-bottom-left-radius: 20px;border-top-left-radius: 20px;">
                          <option >listar por Medico</opction>
                          <option value="leer">Listar todos</option>
                          <?php

                            foreach (parent::get_listaProfesionales() as $profesional)
                            {
                                echo "<option value='".$profesional['id']."'>".$profesional['nombre']."</option>";
                            }
                          
                          ?>
                        </select>
                        <select id="listtipo" onchange="selectTipo();" style="background: #0563af;color: white;padding: 10px;width: 230px;height: 50px;border: none;font-size: 20px;box-shadow: 0 5px 25px rgba(0, 0, 0, .5);cursor: pointer;border-bottom-left-radius: 20px;border-top-left-radius: 20px;float: right;">
                          <option >listar por Tipo</opction>
                          <option value="leer">Listar todos</option>
                          <option style="background:#52af52" value="52af52">Normal</option>
                          <option style="background:#ff0000" value="ff0000">Urgente</option>
                          <option style="background:#ffff0070" value="ffff0070">Prioritario</option>
                          <option style="background:#5b5be2" value="5b5be2">Pendiente</option>
                          <option style="background:#e03197" value="e03197">Particular</option>
                          <option style="background:#e67e7e" value="e67e7e">Obra Solcial</option>
                          <option style="background:#30E8BF" value="30E8BF">Atendido</option>
                          <option style="background:#f5af19" value="f5af19">Rechazado</option>
                          <option style="background:#c31432" value="c31432">Eliminado</option>
                        </select>
                      </div>                        
                  <br>
                  <!-- Renderisa el Calendario -->
                  <div id="calendarioweb"></div>
                  <!-- ------------------------- -->
                </div>
                <div class="col"></div>
          </div>
      </div>
      

    <!-- Bootstrap JavaScript Libraries -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js"></script>

  <!-- Modal(ABM) -->
<div class="modal fade" id="abm" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="c"></h5>
        <button type="button" id="closeModal2" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">

          <span>Turno registrado con Nro: <h4 class='txtId' id="txtId">..</h4></span> 
          <input type="hidden" name="txtFecha" id="txtFecha">

          <div class="form-row">
              <div class="form-group col-md-8">
                <label for="">Razon:</label>
                <input type="text" name="txtTitulo" id="txtTitulo" class="form-control" placeholder="Titulo">
              </div>
              <div class="form-group col-md-8">
                <label for="">Paciente:</label>
                <select name="txtPaciente" id="txtPaciente" style="height: 36px; width: 300px;">
                  <option value="-1" selected>Selecciona ...</opction>
                  <?php
                            
                            foreach (parent::get_listaPacientes() as $paciente)
                            {
                                echo "<option value='".$paciente['id']."'>".$paciente['nombre']." - OS: ".$paciente['oSocial']."</option>";
                            }
                          
                  ?>
                </select>
              </div>
              <div class="form-group col-md-8">
                <label for="">Profesional:</label>
                <select name="txtProfesional" id="txtProfesional" style="height: 36px; width: 300px;">
                  <option value="-1" selected>Selecciona ...</opction>
                  <?php 
                            foreach (parent::get_listaProfesionales() as $profesional)
                            {
                                echo "<option value='".$profesional['id']."'>".$profesional['nombre']." (".$profesional['tipo'].")</option>";
                            }
                          
                  ?>
                </select>
              </div>
              <div class="form-group col-md-4">
                <label for="">Hora: </label>
                <div class="input-group clockpicker" data-autoclose="true">
                  <input type="text" name="txtHora" id="txtHora" value="00:00" class="form-control">
                </div>                
              </div>
          </div>

            <div class="form-group">
              <label for="">Descripcion:</label>
              <textarea name="txtDesc" id="txtDesc" rows="3" class="form-control" placeholder="..."></textarea>
            </div>
            <div class="form-control">
              <label for="">Estado del Turno:</label>
              <select name="txtColor" id="txtColor" style="height: 36px; width: 300px;">
                  <option value="-1" selected>Selecciona ...</opction>
                  <option style="background:#52af52" value="#52af52">Normal</option>
                  <option style="background:#ff0000" value="#ff0000">Urgente</option>
                  <option style="background:#ffff0070" value="#ffff0070">Prioritario</option>
                  <option style="background:#5b5be2" value="#5b5be2">Pendiente</option>
                  <option style="background:#e03197" value="#e03197">Particular</option>
                  <option style="background:#e67e7e" value="#e67e7e">Obra Solcial</option>
                  <optgroup label="Otros">
                      <option style="background:#30E8BF" value="#30E8BF">Atendido</option>
                      <option style="background:#f5af19" value="#f5af19">Rechazado</option>
                  </optgroup>


              </select> 
            </div>       
        
      </div>
      <div class="modal-footer">
          <button type="button" id="btnAgregar" class="btn btn-success">Agregar</button>
          <button type="button" id="btnModificar" class="btn btn-success">Modificar</button>
          <button type="button" id="btnEliminar" class="btn btn-danger">Borrar</button>
          <button type="button" id="closeModal" class="btn btn-default" data-bs-dismiss="modal">Cancelar</button>
        
      </div>
    </div>
  </div>
</div>

<script src="js/calendarioConfing.js"></script>
<script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>


  </body>
</html>