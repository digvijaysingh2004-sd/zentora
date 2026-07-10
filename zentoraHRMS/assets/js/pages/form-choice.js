document.addEventListener("DOMContentLoaded", () => {
    var configDefaults = {
        searchEnabled: true,
        shouldSort: false
    };

    function initChoices(el) {
        if (el.choicesInstance) {
            return; // Already initialized
        }

        var config = {
            placeholderValue: el.hasAttribute("data-choices-groups") ? "This is a placeholder set in the config" : undefined,
            searchEnabled: el.hasAttribute("data-choices-search-true") || !el.hasAttribute("data-choices-search-false"),
            removeItemButton: el.hasAttribute("data-choices-removeItem") || el.hasAttribute("data-choices-multiple-remove"),
            shouldSort: !el.hasAttribute("data-choices-sorting-false"),
            maxItemCount: el.getAttribute("data-choices-limit") || undefined,
            duplicateItemsAllowed: !el.hasAttribute("data-choices-text-unique-true"),
            addItems: !el.hasAttribute("data-choices-text-disabled-true")
        };

        // Merge with defaults
        config = Object.assign({}, configDefaults, config);

        var instance = new Choices(el, config);
        el.choicesInstance = instance;

        if (el.hasAttribute("data-choices-text-disabled-true")) {
            instance.disable();
        }

        // Observe changes to the options inside the select element
        var observer = new MutationObserver((mutations) => {
            observer.disconnect(); // Temporary disconnect to prevent loop
            
            if (el.choicesInstance) {
                var selectedVal = el.value;
                el.choicesInstance.destroy();
                
                el.choicesInstance = new Choices(el, config);
                if (selectedVal) {
                    el.choicesInstance.setChoiceByValue(selectedVal);
                }
                if (el.hasAttribute("data-choices-text-disabled-true")) {
                    el.choicesInstance.disable();
                }
            }
            
            observer.observe(el, { childList: true });
        });

        observer.observe(el, { childList: true });
    }

    var elements = document.querySelectorAll("[data-choices]");
    if (elements && elements.length > 0) {
        elements.forEach(el => {
            initChoices(el);
        });
    }

    // Also watch for dynamically added elements (like inside modals)
    var bodyObserver = new MutationObserver(() => {
        var elements = document.querySelectorAll("[data-choices]");
        if (elements && elements.length > 0) {
            elements.forEach(el => {
                initChoices(el);
            });
        }
    });
    bodyObserver.observe(document.body, { childList: true, subtree: true });
});