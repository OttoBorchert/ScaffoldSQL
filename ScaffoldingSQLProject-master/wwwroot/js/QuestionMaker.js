/* ============================================================================================
                                    QuestionMaker.js
                                  File Type: Javascript
   ============================================================================================
                        Provides Services to the QuestionMaker page
   ============================================================================================
*/



// HTML for each parsons hint
const ParsonsFormFrow = `
<div class="custom-control custom-switch">
    <input type="checkbox" class="custom-control-input" id="SwitchNeedsSingle[%ID%]" name="NeedsSingle[%ID%]" checked>
    <label class="custom-control-label" for="SwitchNeedsSingle[%ID%]">
        Column headers and table names?
    </label>
</div>
<div class="form-row">
    <div class="col-5">
        <select id="%ID%_CommandSelect" class="col form-control form-control-lg" name="Commands[%ID%]" required>
            <option value="" selected disabled hidden>--Please Select a Command--</option>
            <option>SELECT</option>
            <option>FROM</option>
            <option>AS</option>
            <option>;</option>

            <optgroup label="Logical Operations">
                <option>HAVING</option>
                <option>WHERE</option>
            </optgroup>

            <optgroup label="Join Operations">
                <option>JOIN</option>
                <option>CROSS JOIN</option>
                <option>RIGHT JOIN</option>
                <option>FULL OUTER JOIN</option>
                <option>LEFT JOIN</option>
                <option>NATURAL JOIN</option>
                <option>ON</option>
                <option>UNION</option>
                <option>UNION ALL</option>
                <option>USING</option>
            </optgroup>

            <optgroup label="Modify Table">
                <option>DELETE FROM</option>
                <option>DROP TABLE</option>
                <option>INSERT INTO</option>
                <option>REPLACE INTO</option>
                <option>SET</option>
                <option>UPDATE</option>
                <option>VALUES</option>
            </optgroup>

            <optgroup label="Group/Sort">
                <option>ASC</option>
                <option>DESC</option>
                <option>GROUP BY</option>
                <option>LIMIT</option>
                <option>OFFSET</option>
                <option>ORDER BY</option>
            </optgroup>

            <optgroup label="Subquery">
                <option>(</option>
                <option>)</option>
            </optgroup>
        </select>
    </div>
    <div class="col-6">
        <input class="btn-block h-100" id="%ID%_RemoveParsonsRow" type="button" value="Remove Parsons Hint" />
    </div>
</div>

<div id="%ID%_ValueSets" class="mb-1">
    <!-- Do not delete! -->
</div>
<div class="col-5">
    <input id="%ID%_AddValueSet" class="btn-block h-100" type="button" data-RowNumber="1" data-ParsonsID="%ID%" data-is-for-tables="false" value="Add Value Set" data-place-holder="Please wait" />
</div>
`;

// HTML for each test case row
const TestCaseRow = `
<label>Value at specific row, column</label>
<div class="form-row">
    <div col="col-4">
        <input id="%ID%_Row" class="form-control" type="number" min="0" name="TestcaseRow[%ID%]" placeholder="Row Number" />
    </div>
    &nbsp;
    <div id="%ID%_Column" col="col-4">
        <input class="form-control" type="number" min="0" name="TestcaseCol[%ID%]" placeholder="Column Number" />
    </div>
    &nbsp;
    <div col="col-4">
        <input class="form-control" type="text" name="TestcaseText[%ID%]" placeholder="Value in Cell" />
    </div>
    &nbsp;
    <input id="%ID%_Button" class="col-7 form-control mt-1" type="button" value="Remove Test Case" />
</div>
<div class="custom-control custom-switch">
    <input id="%ID%_slider" type="checkbox" class="custom-control-input" name="TestcaseActive[%ID%]" />
    <label class="custom-control-label" for="%ID%_slider">
        Turn this on if you want to check for a column header.
    </label>
</div>`;

/**
 * Add a test case to the question maker page and set to default settings.
 * @param {any} ev
 */
let AddTestCaseToQuestionMaker = function (ev) {
    let parentNode = document.getElementById("rowColumnValueFormGroup");
    let node = document.createElement('div');
    let id = 'TestCaseRow' + parentNode.dataset.rownumber;
    node.id = id;
    node.innerHTML = TestCaseRow.replace(/%ID%/g, id);
    document.getElementById("rowColumnValueFormGroup")
        .insertBefore(node, document.getElementById("addRowColumnValueFormGroup"));
    // Add remove button
    document.getElementById(id + "_Button").addEventListener('click', ev => ev.srcElement.parentNode.parentNode.remove());
    // Add onclick to hide the column check.
    document.getElementById(id + "_slider").addEventListener('click', ev => {
        // get the id of the element to show/hide.
        let id = ev.srcElement.id.replace("_slider", "_Column");
        // Check if checkbox is ticked.
        let data = ev.srcElement.checked;
        // Update elements
        document.getElementById(id).style.visibility = !data ? "visible" : "collapse";
        document.getElementById(id.replace("_Column", "_Row")).placeholder = !data ? "Row Number" : "Column Number";
    });
    parentNode.dataset.rownumber = parseInt(parentNode.dataset.rownumber) + 1;
    return node;
};

/**
 * Clear the value sets and to a number of rows, and then enable or disable the remove value set button.
 * @param {string} id The base id of the node
 * @param {HTMLCollection} valuesets The set of nodes to update
 * @param {number} finalRows NUmber of resulting rows that will be visible by default
 */
let clearValueSets = function (id, valuesets, finalRows) {
    // Remove extranious children
    for (i = valuesets.length - 1; i >= 0; --i) {
        if (valuesets[i].type !== "button") {
            valuesets[i].remove();
        }
    }
    // If there are not enough children, add them
    while (valuesets.length < finalRows) {
        document.getElementById(id + "_AddValueSet").click();
    }
    // Reset text field (Can't use .foreach, sadly)
    for (i = 0; i < finalRows; ++i) {
        valuesets[i].children[0].value = "";
    }
};

/**
 * 
 * @param {any} ev
 */
let OnParsonsCommandChange = function (ev) {
    let id = ev.srcElement.id.replace("_CommandSelect", "");
    let valueSets = document.getElementById(id + "_ValueSets");
    let btnAddValueSet = document.getElementById(id + "_AddValueSet");

    let setFields = (isForTables, showValueSetBtn, showRemoveButton, numOfValSets, placeholder = '', btnValue = '') => {
        if (showValueSetBtn === true) {
            btnAddValueSet.classList.remove("hide");
        } else {
            btnAddValueSet.classList.add("hide");
        }
        
        valueSets.dataset.showRemove = showRemoveButton;
        btnAddValueSet.dataset.placeHolder = placeholder;
        btnAddValueSet.dataset.isForTables = isForTables;
        btnAddValueSet.value = btnValue;
        //clearValueSets(id, valueSets.children, numOfValSets);
    }

    switch (ev.srcElement.value) {
        case "UNION": case 'UNION ALL': case "ASC": case "DESC": case "(": case ")": case ';':
            // Hide all fields: These operators must be used with another operator
            setFields(false, false, "None", 0, "Condition");
            break;
        case "INSERT INTO":
            // Unbounded value sets with table name. Valuesets are optional.
            setFields(true, true, "All", 0, "Column Name", "Add Column Set");
            break;
        case "USING":
            setFields(true, true, "But First", 1, "Column Name", "Add Column Set");
            break;
        case 'SELECT': case "VALUES": case "ADD COLUMN": case "SET": case "FROM":
            // Unbounded value sets without table name
            setFields(false, true, "But First", 1, "Values", "Add Value Set");
            break;
        case "DELETE FROM": case "DROP TABLE":
            // Only edit table
            setFields(false, false, "None", 1, "Add Table Names");
            break;
        case "AS": case "TO": 
            setFields(true, false, "None", 1, "Add Table Names");
            break;
        case "WHERE": case "ON":
            setFields(false, true, "But First", 1, "Conditional", "Add Conditional Component");
            break;
        default:
            // Hide table names and the add value set button
            setFields(true, false, "None", 1, "Add Values");
            break;
    }
};

/**
 * Add a parsons value set to the question maker on the click of a button
 * @param {MouseEvent} ev
 */
let AddParsonsValue = function (ev) {
    let num = parseInt(ev.srcElement.dataset.rownumber);
    let textField = ('<div class="col-5"><input class="btn-block" type="text" placeholder="'
        + ev.srcElement.dataset.placeHolder
        + '" name="CmdOptions[%ID%][value0]" /></div>').replace(/%ID%/, ev.srcElement.dataset.parsonsid).replace(/value0/, (ev.srcElement.dataset.isForTables === "true" ? "tables" : "value") + num);
    const rmvBtn = '<div class="col-5"><input type="button" class="btn-block" value="Remove Value Set" name="RemoveValueSet"/></div>'

    // Create the HTML node
    let node = document.createElement('div');
    node.className = "pt-1 form-row";

    // Get the value sets
    let valueSets = document.getElementById(ev.srcElement.id.replace("AddValueSet", "ValueSets"));
    let show = valueSets.dataset.showRemove;

    let html = textField;

    // Should we show the remove button?
    if (
        (valueSets.children.length === 0 && show === "All") ||
        (valueSets.children.length > 0 && show !== "None")
    ) {
        // Show it
        html += rmvBtn;
    }

    node.innerHTML += html; // Set html
    // Set the event listener on the button that was just added fi possible.
    node.getElementsByTagName('input')[1]?.addEventListener('click', ev => {
        ev.srcElement.parentNode.parentNode.remove();
    });

    valueSets.appendChild(node);
    ev.srcElement.dataset.rownumber = num + 1;
};

/**
 * Add a parsons hint to the QuestionMaker.
 * @param {MouseEvent} ev
 */
let AddParsonsRow = function (ev) {
    const parentNode = document.getElementById("ParsonsProblemForum");
    let id = "FormRowParsonsProblem" + ev.srcElement.dataset.rownumber;
    let node = document.createElement('div');
    node.innerHTML = ParsonsFormFrow.replace(/%ID%/g, id);
    node.id = id;
    parentNode.insertBefore(node, document.getElementById("btnAddParsonsHint"));
    document.getElementById(id + "_CommandSelect").addEventListener('change', OnParsonsCommandChange);
    document.getElementById(id + "_CommandSelect").required = true;
    document.getElementById(id + "_RemoveParsonsRow").addEventListener('click', ev => ev.srcElement.parentNode.parentNode.parentNode.remove());
    document.getElementById(id + "_AddValueSet").addEventListener('click', AddParsonsValue);
    // Work around for C# layer.
    document.getElementById("SwitchNeedsSingle[" + id + "]").checked = false;
    ev.srcElement.dataset.rownumber = parseInt(ev.srcElement.dataset.rownumber) + 1;
};

/**
 * Load and existing question. 
 **/
function loadExisting(filename = null) {
    // Ask the file controller for information on the question
    $.ajax({
        url: '/FileController/GetQuestion',
        type: 'POST',
        data: {
            __RequestVerificationToken: $('[name="__RequestVerificationToken"]').val(),
            File: filename ?? `${document.getElementById("unitNumber").value}.${document.getElementById("questionNumber").value}.txt`
        },
        success: function (result) {
            // Get some of the buttons.
            let btnAddParsonsHint = document.getElementById('btnAddParsonsHint');
            let btnAddTestCase = document.getElementById('addRowColumnValueFormGroup');

            // Set question
            document.querySelector('textarea[name=Question]').value = result['Question'];

            // Decrypt the secret words
            function Decrypt(id, value) {
                let textbox = document.getElementById(id);
                let out = [];
                value.split(', ').forEach(word => {
                    let w = CryptoJS.AES.decrypt(word, "SQL is the best").toString(CryptoJS.enc.Utf8);
                    out.push(w === '' ? word : w);
                });
                textbox.value = out.join(', ');
            }

            Decrypt('notHashedYet', result['SecretWord']);
            Decrypt('notHashedYetPP', result['SecretWordParson']);

            /**
             * Set value in the drop down if possible
             * @param {HTMLSelectElement} selectHtmlNode
             * @param {RegExp} desiredValue
             * @param {Function<RegExp>} unableToFind
             */
            function SetInDropDown(selectHtmlNode, desiredValue, unableToFind = null) {
                let deselect = selectHtmlNode.value !== (desiredValue);
                if (deselect) {
                    let deselectedNode = null;
                    let childrenNodes = selectHtmlNode.getElementsByTagName('option');
                    for (let i = 0; i < childrenNodes.length && deselectedNode === null; ++i) {
                        let option = childrenNodes[i];
                        // If the value is the same
                        if (option.value === desiredValue) {
                            childrenNodes[i].selected = true;
                            selectHtmlNode.dispatchEvent(new Event('change'));
                            return true;
                        }
                    }
                } else {
                    return true;
                }
                unableToFind(desiredValue);
                return false;
            }


            /**
             * Clear out all elements before node
             * @param {any} node
             */
            function clearBeforeNode(node) {
                while (node.previousSibling) {
                    node.previousSibling.remove();
                }
            }


            function AddTestCase(value) {
                // Get the first component before the numbers
                let type = value.match(/.+? /)[0].trim();
                let values = value.replace(type, '').trim();
                let txtrow, txtcol, txtval, switchColumn;
                let matches, match;

                function TestCaseInitLocals() {
                    btnAddTestCase.click();
                    switchColumn = document.querySelector(`#${btnAddTestCase.previousSibling.id} input[type='checkbox']`);
                    txtrow = document.querySelector(`#${btnAddTestCase.previousSibling.id} input[name^='TestcaseRow']`);
                    txtcol = document.querySelector(`#${btnAddTestCase.previousSibling.id} input[name^='TestcaseCol'`);
                    txtval = document.querySelector(`#${btnAddTestCase.previousSibling.id} input[name^='TestcaseText'`);
                    txtSecret = document.getElementById('notHashedYet');
                    txtSecretParsons = document.getElementById('notHashedYetPP');
                }

                // Parse the type of test case
                switch (type) {
                    case "L":
                        if (values[0] === 'R') {
                            document.getElementsByName('TestCaseNumberOfRows')[0].value = values.slice(2);
                        } else if (values[0] === 'C') {
                            document.getElementsByName('TestCaseNumberOfCols')[0].value = values.slice(2);
                        } else {
                            alert(`Unable to find operand ${values[0]} in test case ${value}`);
                            throw `Unable to find operand ${values[0]} in test case ${value}`;
                        }
                        break;
                    case "C":
                        TestCaseInitLocals();
                        switchColumn.checked = true;
                        switchColumn.dispatchEvent(new Event('click'));
                        txtrow.value = values.match(/[0-9]+/)[0].trim();
                        matches = values.match(/(=|>|<)\s.*/);
                        match = matches[0].replace(/(=|>|<)/, '');
                        txtval.value = match.trim();
                        break;
                    case "V":
                        TestCaseInitLocals();
                        txtrow.value = values.match(/[0-9]+/)[0].trim();
                        txtcol.value = values.match(/,[0-9]+/)[0].substring(1).trim();
                        txtval.value = values.match(/(==|>(=|)|<(=|)|!=).+/)[0].substring(2).trim();
                        break;
                    default:
                        alert("Invalid symbol in source file. Is SQL Scaffolding up to date?");
                        break;
                }
            }


            function AddParsons(value) {
                // Get the values
                let valuestrimed = value.trim();
                btnAddParsonsHint.click();
                let node = btnAddParsonsHint.previousSibling;
                let select = document.querySelector(`#${node.id} select`);
                let operator = '';

                // Get the operator in the drop down by remoing one word at a time untile we have a match
                let hasMatch = false;
                // Split on all spaces not between $$toggle and $$
                let operatorWords = valuestrimed.toUpperCase().split(/(?<!\$\$toggle.*)\s+|\s+(?!.*\$\$)/g);
                while (!hasMatch && operatorWords.length > 0) {
                    operator = operatorWords.join(' ');
                    if (SetInDropDown(select, operator, () => { })) {
                        hasMatch = true;
                    } else {
                        operatorWords = operatorWords.slice(0, operatorWords.length - 1)
                    }
                }

                // Erm... couldnot find it. Throw an error and stop.
                if (!hasMatch) {
                    alert(`Unable to find option in select menu for ${value}. Is this file up to date?`);
                    throw `Unable to find option in select menu for ${value}. Is this file up to date?`;
                }

                let valuesets = [], buffer = [], word = '';
                valuestrimed = valuestrimed.split(/\s+/).slice(operatorWords.length).join(' ');

                // Do some preprocessing on the data if required
                switch (operator) {
                    case "VALUES": case 'USING':
                        valuestrimed = valuestrimed.slice(1, valuesets.length - 1);
                        valuesets = valuestrimed.split(/(?<!\$\$toggle.*),|,(?!.*\$\$)/g);
                        break;
                    case 'INSERT INTO': 
                        valuesets = valuestrimed.split(/(?<!\$\$toggle.*),|,(?!.*\$\$)/g);
                        break;
                    case 'SELECT': case 'SET':
                        buffer = valuestrimed.split(/(?<!\$\$toggle.*),|,(?!.*\$\$)/g);
                        // Combine valuesets
                        for (let i = 0; i < buffer.length;) {
                            word = ''; // clear word
                            do {
                                word += (word ? ',' : '') + buffer[i];
                                ++i;
                                // while (word.countUnescaped('(') != word.countUnescaped(')' && i < buffer.length)
                            } while (word.split(/(?:[^\\]|^)\(/g).length !== word.split(/(?:[^\\]|^)\)/g).length && i < buffer.length);
                            valuesets.push(word); // Add valueset
                        }
                        break;
                    default:
                        // No preprocessing nedded.
                        valuesets = valuestrimed.split(/(?<!\$\$toggle.*)\s+|\s+(?!.*\$\$)/g);
                        break;
                }

                // Remove empty values
                valuesets = valuesets.filter(str => str && !/^\s*$/.test(str));

                // Begin writing value sets
                if (valuesets && valuesets.length > 0) {
                    let id = select.id.replace(/CommandSelect/, '');
                    let addValueSet = document.getElementById(id + "AddValueSet");
                    let valuesetDiv = document.getElementById(id + "ValueSets");
                    let remElmments = valuesetDiv.children.length;
                    // Create Value sets
                    for (let i = 0; i < valuesets.length; ++i) {
                        // Add the value set
                        if (remElmments === 0) {
                            addValueSet.click();
                        } else {
                            --remElmments;
                        }
                    }
                    // Get the inputs that are not disallowed
                    let inputs = document.querySelectorAll('#' + id + "ValueSets input[type=text]");
                    // Initialize the valueset
                    for (let i = 0; i < valuesets.length; ++i) {
                        let set = DesanitizeHTMLParsons(valuesets[i].replace(/(\$\$toggle::|\$\$)/g, '').replace(/::/g, ', '));
                        // Set the displayed value
                        let inputTextField = inputs[i];
                        inputTextField.value = set;
                        // Trigger the event change to prevent headaches on future development.
                        inputTextField.dispatchEvent(new Event('change'));
                    }
                }
            }


            // Clear Rows
            clearBeforeNode(btnAddParsonsHint);
            clearBeforeNode(btnAddTestCase);

            // Add test cases
            result["TestCases"].forEach(AddTestCase);

            // Add parsons hints
            result["Parsons"].forEach(AddParsons);

            // Select the database
            SetInDropDown(document.getElementById('databases'), result["Database"][0], _ => alert(`Unable to find the database '${database}'. Is it in the 'Resources/databases' folder?`));
        }
    });
}


document.getElementById("addRowColumnValueFormGroup").addEventListener('click', AddTestCaseToQuestionMaker);
document.getElementById("btnAddParsonsHint").addEventListener('click', AddParsonsRow);