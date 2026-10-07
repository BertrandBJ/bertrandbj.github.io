                                        // =============================================================//
                                        //  Position de défilement (ouverture / fermeture de la modale) //
                                        // =============================================================//


window.portfolioScroll = {
    getY: () => window.scrollY,
    restoreY: (y) => window.scrollTo({ top: y, behavior: "instant" })
};


                                        // =========================================================//
                                        //               Allumage des néons au scroll, c'est sexy
                                        // =========================================================//

const reduceMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches; // c'est un paramètre choisi par l'utilisateur sur sa machine, on le stocke ici dans reduceMotion.

// Posée dès le chargement du script, avant que Blazor affiche quoi que ce soit,
// pour éviter que les sections s'affichent une fraction de seconde puis disparaissent
                                        
if (!reduceMotion) { // => s'il n'y a pas de reduceMotion, aka => c'est bon, tu peux faire des animations : ajoute js-reveal à <html>
    document.documentElement.classList.add("js-reveal"); // documentElement => c'est <html>.
}

setTimeout(() => { // active tout si jamais l'annimation fonctionne pas
    if (revealObserver === null) {
        document.documentElement.classList.remove("js-reveal");
    }
}, 5000);

let revealObserver = null; // création de l'observer, enfin réservation. Comme les éléments à observer n'existent pas encore, il est vide au départ.

const IGNITION_GAP_MS = 280; // écart entre deux allumages successifs, en ms.
let nextIgnition = 0;        // moment (en ms depuis le chargement de la page) où le prochain tube pourra s'allumer => c'est la file d'attente.
const MAX_WAIT_MS = 600;

window.portfolioEffects = {

    initReveal: function () { // ici initReveal c'est une clé, et la fonction la valeur. Donc on a bien des duos clé-valeur.
        if (reduceMotion) {
            return;
        }

        revealObserver = new IntersectionObserver(onReveal, { // IntersectionObserver c'est un truc dispo en JS sur tous les navigateurs, comme document ou d'autres trucs très standard. Attention, c'est EN JS.
            rootMargin: "0px 0px -10% 0px" // ROOT = tout l'écran btw. Ici on rétrécit la zone de détection de 10 % en bas. Quand l'objet entre dans la zone de détection => go lancer onReveal.
        });

        window.portfolioEffects.observeReveal();
    },

    observeReveal: function () {
        if (revealObserver === null) {
            return;
        }

        const elements = document.querySelectorAll(".reveal:not(.is-visible)");

        for (const element of elements) {
            revealObserver.observe(element);
        }
    }
};

function onReveal(entries, observer) { // en JS on peut créer les fonctions après, même si on les appelle avant (elles sont remontées en haut).
    const now = performance.now(); // horloge du navigateur : ms écoulées depuis le chargement de la page.

    for (const entry of entries) {
        if (!entry.isIntersecting) {
            continue;
        }

        const start = Math.max(now, Math.min(nextIgnition, now + MAX_WAIT_MS));// tout de suite si personne n'attend, sinon après le dernier tube de la file.

        entry.target.style.setProperty("--reveal-delay", `${Math.round(start - now)}ms`); // variable CSS => lue à la fois par le panneau et par son ::before.
        entry.target.classList.add("is-visible");
        observer.unobserve(entry.target);

        nextIgnition = start + IGNITION_GAP_MS; // le prochain tube devra attendre son tour.
    }
}