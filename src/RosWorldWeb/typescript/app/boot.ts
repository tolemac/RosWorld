
angular.module("app", ["ngMaterial"]);

System.import("app/game-controller").then(() => {
    angular.bootstrap(document, ["app"]);
});

