#include<Windows.h>
#include"resource.h"

CONST CHAR g_sz_INVITE[] = "Введите имя пользователя";

//Прототип функции:
BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInst, LPSTR lpCmdLine, INT nCmdShow)
{
	DialogBoxParam(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), NULL, DlgProc, NULL);
}

//Реализация функции:
BOOL CALLBACK DlgProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	/*
	------------------------------------------------
	HWND hwnd - Handler to Window. 
	Дескриптор окна - переменная, через которую можно обратиться к окну.
	GetDlgItem(hwnd, IDC_);
	hwnd - родительское окно, на котором расположен элемент управления;
	IDC_ - это ResourceID элемента управление, которые назначается в дизайнере:
			IDC_EDIT_LOGIN;
			IDC_EDIT_PASSWORD;
			IDC_BUTTON_COPY;
	------------------------------------------------
	UINT uMsg - Message.
	Сообщение, при помощи которого мы просим окно что-то сделать, либо спрашиваем у него какую-то информацию,
	например:
		- чтобы задать текст окна, нужно отправить ему сообщение WM_SETTEXT;
		- чтобы получить текст окна, нужно отправить ему сообщение WM_GETTEXT;
		- чтобы задать иконку окна, нужно отправить ему сообщение WM_SETICON;
		WM_ - Window Message. Применимы к любым окнам;
		EM_ - Edit Message. Применимо только к окнам EditControl (текстовое поле);
		EN_ - Edit Notification. Уведомление от EditControl;
		Сообщения - это святая святых Windows. Каждую секунду Windows отправляет тысячи сообщений.
		Сообщения - это основное средство коммуникации между окнами.
		Для того чтобы отправить сообщение окну используется функция SendMessage(),
		Которая, так же как и процедура окна всегда принимает 4 параметра:
		(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
		uMsg - uMessage (u - Unsigned, UINT - Unsigned INT, т.е. беззнаковый - только положительные числа)
	------------------------------------------------
	WPARAM wParam, LPARAM lParam - параметры СООБЩЕНИЯ,
	они всегда зависят от самого сообщения,
	у каждого сообщения свои параметры.
	WPARAM и LPARAM занимают 4 Байта и часто делятся на HIWORD и LOWORD.
	HIWORD - старшее слово;
	LOWORD - младшее слово;
	------------------------------------------------
	*/
	switch (uMsg)
	{
	case WM_INITDIALOG:	//Отрабатывает один раз, при запеске окна
	{
		HICON hIcon = LoadIcon(GetModuleHandle(NULL), MAKEINTRESOURCE(IDI_ICON1));
		SendMessage(hwnd, WM_SETICON, 0, (LPARAM)hIcon);
		HWND hEditLogin = GetDlgItem(hwnd, IDC_EDIT_LOGIN);	//Получаем дескриптор текстового поля 'Login', для того чтобы отправить ему сообщение.
		SendMessage(hEditLogin, WM_SETTEXT, 0, (LPARAM)g_sz_INVITE);
	}
		break;
	case WM_COMMAND:	//Обработка нажатия кнопок, действий мышью, и т.д.
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON_COPY:
		{
			HWND hEditLogin = GetDlgItem(hwnd, IDC_EDIT_LOGIN);
			HWND hEditPassword = GetDlgItem(hwnd, IDC_EDIT_PASSWORD);
			CONST INT SIZE = 256;
			CHAR szBuffer[SIZE] = {};
			SendMessage(hEditLogin, WM_GETTEXT, SIZE, (LPARAM)szBuffer);
			SendMessage(hEditPassword, WM_SETTEXT, 0, (LPARAM)szBuffer);
		}
			break;
		case IDOK: MessageBox(hwnd, "Была нажата кнопка OK!", "Info", MB_OK | MB_ICONINFORMATION); break;
		case IDCANCEL: EndDialog(hwnd, 0);
		}
		break;
	case WM_CLOSE:		//Отрабатывает при нажатии на кнопку закрытия окна 'X'
		EndDialog(hwnd, 0);
		break;
	}
	return FALSE;
}