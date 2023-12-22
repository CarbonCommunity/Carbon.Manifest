using Rust.UI;
using UnityEngine;
using UnityEngine.UI;

public class PhoneDialler : UIDialog
{
	public GameObject DialingRoot = null;

	public GameObject CallInProcessRoot = null;

	public GameObject IncomingCallRoot = null;

	public RustText ThisPhoneNumber = null;

	public RustInput PhoneNameInput = null;

	public RustText textDisplay;

	public RustText CallTimeText = null;

	public RustButton DefaultDialViewButton = null;

	public RustText[] IncomingCallNumber;

	public GameObject NumberDialRoot = null;

	public GameObject PromptVoicemailRoot = null;

	public RustButton ContactsButton = null;

	public RustText FailText = null;

	public NeedsCursor CursorController = null;

	public NeedsKeyboard KeyboardController = null;

	public Translate.Phrase WrongNumberPhrase;

	public Translate.Phrase NetworkBusy;

	public Translate.Phrase Engaged;

	public GameObjectRef DirectoryEntryPrefab = null;

	public Transform DirectoryRoot = null;

	public GameObject NoDirectoryRoot = null;

	public RustButton DirectoryPageUp = null;

	public RustButton DirectoryPageDown = null;

	public Transform ContactsRoot = null;

	public RustInput ContactsNameInput = null;

	public RustInput ContactsNumberInput = null;

	public GameObject NoContactsRoot = null;

	public RustButton AddContactButton = null;

	public SoundDefinition DialToneSfx;

	public Button[] NumberButtons;

	public Translate.Phrase AnsweringMachine;

	public VoicemailDialog Voicemail = null;

	public GameObject VoicemailRoot = null;
}
