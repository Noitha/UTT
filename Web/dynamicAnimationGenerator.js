let unityInstance = UnityLoader.instantiate("unityContainer", "Build/Animation Build.json", {onProgress: UnityProgress});

//The sequence array holding all the data.
let sequenceList = [];

//Class defining the same fields required to generate the JSON-Object.
class Sequence
{
	mechanism;
	animationName;
	parameters;

	constructor(mechanism, animationName, parameters)
	{
		this.mechanism = mechanism;
		this.animationName = animationName;
		this.parameters = parameters;
	}
}

//A list of the available configurations.
const availableConfigurations =
	[
		{
			mechanism : 'Spawn',
			availableAnimation : ['Spawn01'],
			inputs:
				[
					{label : 'Offset', name: 'offset', type : "vector3"},
					{label : 'Rotation', name: 'rotation', type : "float"},
					{label : 'Duration', name: 'duration', type : "float"}
				]
		},
		{
			mechanism : 'Rotate',
			availableAnimation : ['XDirection',],
			inputs:
				[
					{label : 'Amount', name: 'rotation', type : "float"},
					{label : 'Duration', name: 'duration', type : "float"}
				]
		},
		{
			mechanism : 'Walk',
			availableAnimation : ['YDirection',],
			inputs:
				[
					{label : 'Distance', name: 'distance', type : "float"},
					{label : 'Duration', name: 'duration', type : "float"}
				]
		},
		{
			mechanism : 'Talk',
			availableAnimation : ['Talk01',],
			inputs:
				[
					{label : 'Duration', name: 'duration', type : "float"}
				]
		},
		{
			mechanism : 'Sit',
			availableAnimation : ['Sit01',],
			inputs:
				[
					{label : 'Duration', name: 'duration', type : "float"}
				]
		},
		{
			mechanism : 'Lay',
			availableAnimation : ['Lay01'],
			inputs:
				[
					{label : 'Duration', name: 'duration', type : "float"}
				]
		}
	];

//Get the sequence container.
let sequenceContainer = document.getElementById('sequences');

function Redraw()
{
	//Clear the sequence container.
	sequenceContainer.innerHTML = '';

	for (let i = 0; i < sequenceList.length; i++)
	{
		//Create a container holding all the sequences.
		let newSequence = CreateContainer('sequence');
		
		let buttons = CreateContainer('buttons');
		newSequence.appendChild(buttons);
		
		//Add move buttons depending on their index position.
		if(sequenceList.length > 1)
		{
			if(i !== 0 && i !== sequenceList.length - 1)
			{
				buttons.appendChild(CreateButton('Move Up', 'moveButton', 'SwapSequence(' + i + ',' + (i - 1) + ')'));
				buttons.appendChild(CreateButton('Move Down', 'moveButton', 'SwapSequence(' + i + ',' + (i + 1) + ')'));
			}
			else if (i === 0)
			{
				buttons.appendChild(CreateButton('Move Down', 'moveButton', 'SwapSequence(' + i + ',' + (i + 1) + ')'));
			}
			else
			{
				buttons.appendChild(CreateButton('Move Up', 'moveButton', 'SwapSequence(' + i + ',' + (i - 1) + ')'));
			}
		}
		
		buttons.appendChild(CreateButton('x', 'removeSequenceButton', 'RemoveSequence('+ i + ')'));

		//Create a container holding all the input fields.
		let parameterContainer = CreateContainer('parameter-container');
		
		//Create a select of the available mechanisms.
		let selectMechanismType = CreateSelect('mechanismType', 'UpdateMechanism(this, ' + i + ')', GetAllMechanismTypes());
		selectMechanismType.value = sequenceList[i].mechanism;

		//Create a select of all the available animations of the selected mechanism.
		let selectAnimation = CreateSelect('availableAnimation', 'UpdateAnimation(this, ' + i + ')', GetAllAnimationsOfChosenMechanism(sequenceList[i].mechanism));
		selectAnimation.value = sequenceList[i].animationName === "" ? availableConfigurations[GetMechanismIndex(sequenceList[i].mechanism)].availableAnimation[0] : sequenceList[i].animationName;

		parameterContainer.appendChild(CreateLabelForParameter("Mechanism:", selectMechanismType));
		parameterContainer.appendChild(CreateLabelForParameter("Animation:", selectAnimation));
		
		//Did the mechanic changed.
		let mechanicsChanged = sequenceList[i].parameters.length === 0;

		//Get the current configuration.
		let currentConfiguration = availableConfigurations[GetMechanismIndex(sequenceList[i].mechanism)];

		for (let j = 0; j < currentConfiguration.inputs.length; j++)
		{
			let currentInputToGenerate = currentConfiguration.inputs[j];
			let name = currentInputToGenerate.name;
			let label = currentInputToGenerate.label + ":";
			let mechanism = currentConfiguration.mechanism;

			switch (currentInputToGenerate.type)
			{
				case 'float':
					if(mechanicsChanged)
					{
						sequenceList[i].parameters[j] = {name : name, type : "float", value : '0'};
					}
					
					parameterContainer.appendChild(CreateLabelForParameter(label, CreateInputParameter(mechanism + ":" + label, i, mechanicsChanged ? '0' : sequenceList[i].parameters[j].value)));
					break;

				case 'vector3':
					if(mechanicsChanged)
					{
						sequenceList[i].parameters[j] = {name : currentInputToGenerate.name, type : "vector3", value : '0;0;0'};
					}
					
					let previousValues = sequenceList[i].parameters[j].value.split(';');
					parameterContainer.appendChild(CreateLabelForParameter("X-" + label, CreateInputParameter(mechanism + ":X-" + label, i, mechanicsChanged ? '0' : previousValues[0])));
					parameterContainer.appendChild(CreateLabelForParameter("Y-" + label, CreateInputParameter(mechanism + ":Y-" + label, i,  mechanicsChanged ? '0' : previousValues[1])));
					parameterContainer.appendChild(CreateLabelForParameter("Z-" + label, CreateInputParameter(mechanism + ":Z-" + label, i, mechanicsChanged ? '0' : previousValues[2])));
					break;
			}
		}
		
		
		newSequence.appendChild(parameterContainer);
		sequenceContainer.appendChild(newSequence);
	}
}

//Change the input type according to the chosen mechanism.
function UpdateMechanism(mechanismType, index)
{
	sequenceList[index].mechanism = mechanismType.value;
	sequenceList[index].parameters = [];
	Redraw();
}

//Change the input type according to the chosen mechanism.
function UpdateAnimation(animation, index)
{
	sequenceList[index].animationName = animation.value;
}

//Set the parameter value.
function SetSequenceParameterValue(parameter, type, index)
{
	let parameterInfo = type.split(':');

	switch(parameterInfo[0])
	{
		case 'Spawn':
			let values = sequenceList[index].parameters[0].value.split(';');
			let parameters = "";

			switch (parameterInfo[1])
			{
				case 'X-Offset':
					parameters += parameter.value;
					parameters += ";" + values[1];
					parameters += ";" + values[2];
					sequenceList[index].parameters[0] = {name : 'offset', type : "vector3", value : parameters};
					break;

				case 'Y-Offset':
					parameters += values[0];
					parameters += ";" + parameter.value;
					parameters += ";" + values[2];
					sequenceList[index].parameters[0] = {name : 'offset', type : "vector3", value : parameters};
					break;

				case 'Z-Offset':
					parameters += values[0];
					parameters += ";" + values[1];
					parameters += ";" + parameter.value;
					sequenceList[index].parameters[0] = {name : 'offset', type : "vector3", value : parameters};
					break;

				case 'Rotation':
					sequenceList[index].parameters[1] = {name : 'rotation', type : "float", value : parameter.value};
					break;

				case 'Duration':
					sequenceList[index].parameters[2] = {name : 'duration', type : "float", value : parameter.value};
					break;
			}
			break;

		case 'Rotate':

			switch (parameterInfo[1])
			{
				case 'Amount':
					sequenceList[index].parameters[0] = {name : 'rotation', type : "float", value : parameter.value};
					break;

				case 'Duration':
					sequenceList[index].parameters[1] = {name : 'duration', type : "float", value : parameter.value};
					break;
			}
			break;

		case 'Walk':
			switch (parameterInfo[1])
			{
				case 'Distance':
					sequenceList[index].parameters[0] = {name : 'distance', type : "float", value : parameter.value};
					break;

				case 'Duration':
					sequenceList[index].parameters[1] = {name : 'duration', type : "float", value : parameter.value};
					break;
			}
			break;

		case 'Talk':
			sequenceList[index].parameters[0] = {name : 'duration', type : "float", value : parameter.value};
			break;

		case 'Sit':
			sequenceList[index].parameters[0] = {name : 'duration', type : "float", value : parameter.value};
			break;

		case 'Lay':
			sequenceList[index].parameters[0] = {name : 'duration', type : "float", value : parameter.value};
			break;

	}
}

//Create an input element.
function CreateInputParameter(parameterID, sequenceIndex, value)
{
	let input = document.createElement('input');
	input.value = value;
	input.setAttribute('type', 'number');
	input.setAttribute('class', 'parameter-input');
	input.setAttribute('onchange', 'SetSequenceParameterValue(this, ' + '\'' + parameterID + '\'' + ', ' + sequenceIndex + ')');
	return input;
}

//Create a label element.
function CreateLabelForParameter(text, parameter)
{
	let label = document.createElement('label');
	label.setAttribute('class', 'parameter-label');
	
	let span = document.createElement('span');
	span.setAttribute('class', 'parameter-span');
	span.appendChild(document.createTextNode(text));
	
	label.appendChild(span);
	label.appendChild(parameter);
	return label;
}

//Create a container.
function CreateContainer(className)
{
	let container = document.createElement('div');
	container.setAttribute('class', className);
	return container;
}

//Create a button.
function CreateButton(text, className, event)
{
	let button = document.createElement('button');
	button.appendChild(document.createTextNode(text));
	button.setAttribute('class', className);
	button.setAttribute('onclick', event);
	return button;
}

function CreateSelect(className, event, options)
{
	let select = document.createElement('select');
	select.setAttribute('class', className);
	select.setAttribute('onchange', event);

	for (let i = 0; i < options.length; i++)
	{
		//Create a new option and set its value to the mechanism type.
		let option = document.createElement('option');
		option.setAttribute('value', options[i]);

		//Create a new text node and append the value of the mechanism type to it.
		option.appendChild(document.createTextNode(options[i]))

		//Append the option to the select element.
		select.appendChild(option);
	}

	return select;
}

function GetAllMechanismTypes()
{
	let mechanisms = [];

	for (let i = 0; i < availableConfigurations.length; i++)
	{
		mechanisms.push(availableConfigurations[i].mechanism);
	}

	return mechanisms;
}

function GetAllAnimationsOfChosenMechanism(mechanism)
{
	let animations = [];
	let index = GetMechanismIndex(mechanism);

	if(index === -1)
	{
		return animations;
	}

	for (let i = 0; i < availableConfigurations[index].availableAnimation.length; i++)
	{
		animations.push(availableConfigurations[index].availableAnimation[i]);
	}

	return animations;
}

function GetMechanismIndex(mechanism)
{
	for (let i = 0; i < availableConfigurations.length; i++)
	{
		if(availableConfigurations[i].mechanism === mechanism)
		{
			return i;
		}
	}

	return -1;
}

//Generate a JSON text.
function GenerateJSON()
{
	let hotspotAnimations = {HotspotAnimations : sequenceList};
	let jsonOutput = document.getElementById('displayJsonOutput');
	let json = JSON.stringify(hotspotAnimations, null, 4);
	jsonOutput.innerHTML = '<pre>' + json + '</pre>';
}

function Play()
{
	let hotspotAnimations = {HotspotAnimations : sequenceList};
	unityInstance.SendMessage('Dynamic Animation', 'WebJsonAnimationLoader', JSON.stringify(hotspotAnimations));
}

//Add a sequence to the list.
function AddSequence()
{
	let defaultConfiguration = availableConfigurations[0];
	sequenceList.push(new Sequence(defaultConfiguration.mechanism, defaultConfiguration.availableAnimation[0], []));
	Redraw();
}

//Removes a sequence from the list.
function RemoveSequence(index)
{
	let newSequenceList = [];

	for (let i = 0; i < sequenceList.length; i++)
	{
		if(i === index)
		{
			continue;
		}

		newSequenceList.push(sequenceList[i]);
	}

	sequenceList = newSequenceList;
	Redraw();
}

//Swap a sequence with another.
function SwapSequence(sequenceA, sequenceB)
{
	let tmp = sequenceList[sequenceA];
	sequenceList[sequenceA] = sequenceList[sequenceB];
	sequenceList[sequenceB] = tmp;
	Redraw();
}