import { Residencia } from './residencia';

export class Subasta {
    residencia: Residencia;
    valor: number;
    semana: Date;

    constructor(residencia: Residencia, valor: number, semana: Date) {
        this.residencia = residencia;
        this.valor = valor;
        this.semana = semana;
    }

}