                                    // =============================================================//
                                    //  Position de défilement (ouverture / fermeture de la modale)=//
                                    // =============================================================//
                                    
                                    
window.portfolioScroll = {
    getY: () => window.scrollY,
    restoreY: (y) => window.scrollTo({ top: y, behavior: "instant" })
};


                                            // =========================================================//
                                            //              Apparition des éléments au scroll           //
                                            // =========================================================//

const reduceMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches; // c'est un paramètre selected par l'utilisateur sur sa machine, on le stock ici dans reduceMotion.

// Posée dès le chargement du script, avant que Blazor affiche quoi que ce soit,
// pour éviter que les sections s'affichent une fraction de seconde puis disparaissent
                                    
                                    
if (!reduceMotion) { // => si y'a pas le reduceMotion, aka => c'est bon tu peux faire des motions ajoute js-reveal à la <html>
    document.documentElement.classList.add("js-reveal"); // documentElement => c'est <html>.
}

let revealObserver = null; // creation de l'observer, enfin réservation. comme l'element à obs n'existe pas encore il est vide initialy.

window.portfolioEffects = {

    initReveal: function () { // ici initreveal c'est une clé, et la fonction la valeur. Donc on a bien des duos clé-valeurs.
        if (reduceMotion) {
            return;
        }

        revealObserver = new IntersectionObserver(onReveal, { // là du coup IntersectionObserver c'est un truc dispo en JS sur tout les navigateur, comme document ou d'autre truc trrès standard. Alors attention c'est EN JS.//
            rootMargin: "0px 0px -10% 0px" // ROOT = tout l'écran btw, donc ici on lui dit avec 10% de margin en bas pour la zone de detection. quand l'object rentre dans la zone de detection => go lancer on reveal.
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

function onReveal(entries, observer) { // en JS on peut create les functions après, même si on les appels avant. (elles sont remontées en haut). 
    let delayIndex = 0;

    for (const entry of entries) {
        if (!entry.isIntersecting) { // => true si l'élement est dans la zone de détection ? en gros cela fait  : IF l'élement n'est pas à l'écran, go next element.
            continue;
        }
        // si l'element est à l'écran ici// => 
        entry.target.style.animationDelay = `${delayIndex * 80}ms`; // ici target est l'element étudié dans la boucle actuellement, si y'en a plusieurs (aka il fait plusieurs loop) => le delayindex incrémente de 1 et on up de 80ms à chaque loop.
        entry.target.classList.add("is-visible"); 
        observer.unobserve(entry.target); // on unobserve la target de la boucle, elle est déjà "affiché".
        delayIndex++;
    }
}