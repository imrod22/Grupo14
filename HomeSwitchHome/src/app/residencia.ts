export class Residencia {
    title: string;
    ubication: string;
    description: string;
    weeks: Date[] = [];

    constructor(title: string, ubication: string, description: string) {
        this.title = title;
        this.ubication = ubication;
        this.description = description;
        this.initializeWeeks();
    }

    initializeWeeks() {
        var d = 1;
        for (let i = 0; i < 4; i++) {
            this.weeks[i] = new Date(2020, 1, d);
            d += 7;
        }
    }

}