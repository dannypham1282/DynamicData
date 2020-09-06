//jqgrid style setup
$.jgrid.icons.icons4ace = {
    baseIconSet: "fontAwesome5",
    common: "fas",
    actions: {
        edit: "fa-pencil-alt text-blue",
        del: "fa-trash-alt text-danger",
        save: "fa-check text-success",
        cancel: "fa-times text-orange"
    },

    pager: {
        first: "fas fa-fw fa-step-backward fa-lg text-default ",
        prev: "fas fa-fw fa-backward fa-lg text-default",
        next: "fas fa-fw fa-forward fa-lg text-default",
        last: "fas fa-fw fa-step-forward fa-lg text-default "
    },

    gridMinimize: {
        visible: "fa-chevron-up",
        hidden: "fa-chevron-down"
    },

    sort: {
        common: "far",
        asc: "fa-caret-up",
        desc: "fa-caret-down"
    },

    form: {
        close: "fa-times my-2px",
        prev: "fa-chevron-left",
        next: "fa-chevron-right",
        save: "fa-check",
        undo: "fa-times",
        del: "fa-trash-alt",
        cancel: "fa-times",

        resizableLtr: "fa-rss fa-rotate-270 text-orange"
    },
}

$.jgrid.guiStyles.bootstrap4ace = {
    baseGuiStyle: "bootstrap4",
    actionsButton: "action-btn mx-1 px-2px float-none border-0",
    states: {
        select: "bgc-success-l2 bgc-h-success-l1",
        th: "bgc-yellow-l1 text-blue-d2",
        //hoverTh: "bgc-default-l2 text-default-d3",
        hoverTh: "bgc-yellow-m4 text-dark-m3",

        error: "alert bgc-danger-l3",
        //active: "active",
        //textOfClickable: ""
    },

    //dialogs
    overlay: "modal-backdrop",
    dialog: {
        header: "modal-header bgc-default-l4 text-blue-m1 py-2 px-3",
        window: "modal mw-100",
        document: "modal-dialog mw-none",

        content: "modal-content p-0",
        body: "modal-body px-2 py-25 text-130",
        footer: "modal-footer",

        closeButton: "mr-1 mt-n25 px-2 py-1 w-auto h-auto border-1 brc-h-warning-m1 bgc-h-warning-l1 text-danger radius-round",
        fmButton: "btn btn-sm btn-default",

        viewLabel: "control-label py-2",
        dataField: "form-control my-2 ml-1 w-auto",
        viewCellLabel: "text-right w-4 pr-2",
        viewData: "text-left text-secondary-d2 d-block border-1 brc-grey-l2 p-2 radius-1 my-2 ml-1"
    },

    searchDialog: {
        elem: "form-control w-95",
        operator: "form-control w-95",
        label: "form-control w-95",

        addRuleButton: "btn btn-xs btn-outline-primary radius-round btn-bold px-2 mx-1 text-110",
        addGroupButton: "btn btn-xs btn-primary mx-1 text-110",
        deleteRuleButton: "h-4 px-2 pt-0 text-150 mr-1 btn btn-xs btn-outline-danger border-0",
        deleteGroupButton: "h-4 px-2 pt-0 text-150 mr-1 btn btn-xs btn-outline-danger border-0",
    },

    navButton: "action-btn border-0 text-110 mx-1",
    pager: {
        pager: "py-3 px-1 px-md-2 bgc-primary-l4 border-t-1 brc-default-l2 mt-n1px",
        pagerInput: "form-control form-control-sm text-center d-inline-block",
        pagerSelect: "form-control w-6 px-1",
        pagerButton: "p-0 m-0 border-0 radius-round text-110",
    },

    subgrid: {
        button: "", //don't remove
        row: "bgc-secondary-l4 p-0",
    },

    loading: "alert bgc-primary-l3 brc-primary-m2 text-dark-tp3 text-120 px-4 py-3",
}
