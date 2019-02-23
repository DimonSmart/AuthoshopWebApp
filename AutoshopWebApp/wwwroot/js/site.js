// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function makeFromTemplateRecoursive(value, template) {
    let temp = template;

    $.each(value, function (k, v) {
        let typeV = typeof (v);
        const regex = new RegExp('{{' + k + '}}', 'ig')
        const dateISO = /(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})/;

        let dateTest = dateISO.test(v);

        if (dateTest) {
            typeV = 'date';
        }

        switch (typeV) {
            case 'object':
                temp = makeFromTemplateRecoursive(v, temp);
                break;
            case 'date':
                const tempDate = (new Date(v)).toLocaleDateString();
                temp = temp.replace(regex, tempDate);
                break;
            default:
                temp = temp.replace(regex, v);
        }
    });

    return temp;
}