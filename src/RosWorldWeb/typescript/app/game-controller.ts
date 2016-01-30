/// <reference path="../typings/tsd.d.ts" />

class GameController {

    static $inject = ["$http", "$interval"];
    constructor(private $http: ng.IHttpService, $interval : ng.IIntervalService) {
        //this.updateEngine();

        $interval(() => {
            this.updateEngine();
            this.updateEntities();
        }, 500);
    }

    engine: string;
    entities: string;
    player1: string;
    player2: string;

    gameMessage: string;

    updateEngine() {
        this.$http.post("/Game/GetEngine", {}).then((response: any) => {
            this.engine = JSON.stringify(response.data, null, "\t");
        });
    }

    updateEntities() {
        this.$http.post("/Game/GetEntities", {}).then((response: any) => {
            this.entities = JSON.stringify(response.data, null, "\t");
            this.player1 = JSON.stringify(response.data[0], null, "\t");
            this.player2 = JSON.stringify(response.data[1], null, "\t");
        });
    }

    clearMessage() {
        this.setMessage("");
    }

    setMessage(msg) {
        this.gameMessage = msg;
    }

    player1BuildHouse() {
        this.clearMessage();
        this.$http.post("/Game/Player1BuildHouse", {}).then((response: any) => {
            if (response.data !== "True")
                this.setMessage("No tiene oro suficiente para construir casas, cada casa vale 100 de oro y crea 5 personas para coger más oro.");

        });
    }

    player2BuildHouse() {
        this.clearMessage();
        this.$http.post("/Game/Player2BuildHouse", {}).then((response: any) => {
            if (response.data !== "True")
                this.setMessage("No tiene oro suficiente para construir casas, cada casa vale 100 de oro y crea 5 personas para coger más oro.");

        });
    }

    reset() {
        this.clearMessage();
        this.$http.post("/Game/Reset", {});
    }

}

angular.module("app").controller("gameController", GameController);