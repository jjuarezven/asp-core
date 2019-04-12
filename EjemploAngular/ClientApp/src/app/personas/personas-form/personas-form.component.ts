import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { PersonasService } from '../personas.service';
import { IPersona } from 'src/app/models/persona';
import { Router, ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { DireccionesService } from 'src/app/direcciones/direcciones.service';

@Component({
  selector: 'app-personas-form',
  templateUrl: './personas-form.component.html',
  styleUrls: ['./personas-form.component.css']
})
export class PersonasFormComponent implements OnInit {
  formGroup: FormGroup;
  modoEdicion: boolean = false;
  personaId: number;
  direccionesABorrar: number[] = [];

  constructor(private fb: FormBuilder,
    private personasService: PersonasService,
    private direccionesService: DireccionesService,
    private router: Router,
    private activatedRouter: ActivatedRoute) { }

  ngOnInit() {
    this.formGroup = this.fb.group({
      nombre: '',
      fechaNacimiento: '',
      direcciones: this.fb.array([])
    });

    this.activatedRouter.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.modoEdicion = true;
      this.personaId = params["id"];
      this.personasService.getPersona(this.personaId.toString()).subscribe(persona => this.cargarFormulario(persona), error => this.router.navigate(["/personas"]));
    });
  }

  cargarFormulario(persona: IPersona): void {
    const dp = new DatePipe(navigator.language);
    const format = "yyyy-MM-dd";
    this.formGroup.patchValue({
      nombre: persona.nombre,
      fechaNacimiento: dp.transform(persona.fechaNacimiento, format)
    });

    let direcciones = this.formGroup.controls['direcciones'] as FormArray;
    persona.direcciones.forEach(direccion => {
      const direccionFG = this.construirDireccion();
      direccionFG.patchValue(direccion);
      direcciones.push(direccionFG);
    });

  }

  save() {
    const persona: IPersona = Object.assign({}, this.formGroup.value);
    if (this.modoEdicion) {
      persona.id = this.personaId;
      this.personasService.updatePersona(persona).subscribe(() => this.borrarDirecciones(), error => console.log(error));
    } else {
      this.personasService.createPersona(persona).subscribe(() => this.onSaveSuccess(), error => console.log(error));
    }
  }

  borrarDirecciones(): void {
    if (this.direccionesABorrar.length === 0) {
      this.onSaveSuccess();
      return;
    }
    this.direccionesService.deleteDirecciones(this.direccionesABorrar).subscribe(() => this.onSaveSuccess(), error => console.error(error));
  }

  onSaveSuccess(): void {
    this.router.navigate(["/personas"]);
  }

  agregarDireccion(): void {
    const direccionArr = this.formGroup.get('direcciones') as FormArray;
    const direccionesFG = this.construirDireccion();
    direccionArr.push(direccionesFG);
  }

  construirDireccion(): FormGroup {
    return this.fb.group({
      id: 0,
      calle: '',
      provincia: '',
      personaId: this.personaId ? this.personaId : 0
    });
  }

  removerDireccion(index: number): void {
    const direcciones = this.formGroup.get('direcciones') as FormArray;
    const direccionRemover = direcciones.at(index) as FormGroup;
    if (direccionRemover.controls['id'].value !== '0') {
      this.direccionesABorrar.push(<number>direccionRemover.controls['id'].value);
    }
    direcciones.removeAt(index);
  }

}
