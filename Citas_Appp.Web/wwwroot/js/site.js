// site.js — interacciones y animaciones de CitaApp
// Usa GSAP (https://gsap.com) de forma básica: solo gsap.to()/gsap.from()
// y timelines. No se usan plugins extra (como ScrollTrigger) para
// mantenerlo simple.

(function () {
    "use strict";

    var prefersReducedMotion = window.matchMedia(
        "(prefers-reduced-motion: reduce)"
    ).matches;

    var hasGsap = typeof window.gsap !== "undefined";

    document.addEventListener("DOMContentLoaded", function () {
        initNavbarScrollState();
        initHeroAnimation();
        initScrollReveal();
    });

    function initNavbarScrollState() {
        var navbar = document.getElementById("appNavbar");
        if (!navbar) return;

        function toggleScrolled() {
            navbar.classList.toggle("is-scrolled", window.scrollY > 8);
        }

        toggleScrolled();
        window.addEventListener("scroll", toggleScrolled, { passive: true });
    }
    function initHeroAnimation() {
        var items = document.querySelectorAll(".js-anim");
        if (!items.length) return;

        // Si el usuario prefiere menos movimiento, o GSAP no cargó
        // (por ejemplo, sin conexión al CDN), mostramos el contenido
        // directamente sin animar.
        if (prefersReducedMotion || !hasGsap) {
            items.forEach(function (el) {
                el.style.opacity = 1;
            });
            return;
        }

        var tl = gsap.timeline({
            defaults: { ease: "power2.out", duration: 0.7 },
        });

        tl.from('[data-anim="badge"]', { opacity: 0, y: 12 })
            .from('[data-anim="title"]', { opacity: 0, y: 18 }, "-=0.45")
            .from('[data-anim="subtitle"]', { opacity: 0, y: 18 }, "-=0.5")
            .from('[data-anim="actions"]', { opacity: 0, y: 14 }, "-=0.5")
            .from(
                '[data-anim="art"]',
                { opacity: 0, y: 24, scale: 0.97, duration: 0.8 },
                "-=0.6"
            );
    }

    function initScrollReveal() {
        var items = document.querySelectorAll(".js-reveal");
        if (!items.length) return;

        if (prefersReducedMotion || !hasGsap) {
            items.forEach(function (el) {
                el.style.opacity = 1;
            });
            return;
        }

        if (!("IntersectionObserver" in window)) {
            items.forEach(function (el) {
                el.style.opacity = 1;
            });
            return;
        }

        var observer = new IntersectionObserver(
            function (entries) {
                entries.forEach(function (entry) {
                    if (!entry.isIntersecting) return;

                    gsap.to(entry.target, {
                        opacity: 1,
                        y: 0,
                        duration: 0.6,
                        ease: "power2.out",
                    });

                    observer.unobserve(entry.target);
                });
            },
            { threshold: 0.15 }
        );

        items.forEach(function (el) {
            // Estado inicial (antes de entrar en pantalla).
            gsap.set(el, { opacity: 0, y: 24 });
            observer.observe(el);
        });
    }
})();
