window.portfolioScroll = {
    getY: () => window.scrollY,
    restoreY: (y) => window.scrollTo({ top: y, behavior: "instant" })
};