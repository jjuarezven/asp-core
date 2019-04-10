import { Component, OnInit } from '@angular/core';
import { IPersona } from '../models/persona';
import { PersonasService } from './personas.service';

@Component({
  selector: 'app-personas',
  templateUrl: './personas.component.html',
  styleUrls: ['./personas.component.css']
})
export class PersonasComponent implements OnInit {
  personas: IPersona[];

  constructor(private personasService: PersonasService) { }

  ngOnInit() {
    this.cargarData();
  }

  delete(personaId: string): void {
    this.personasService.deletePersona(personaId).subscribe(() => this.cargarData(), error => console.log(error));
  }

  cargarData(): void {
    this.personasService.getPersonas().subscribe(data => this.personas = data, error => console.log(error));
  }

}
