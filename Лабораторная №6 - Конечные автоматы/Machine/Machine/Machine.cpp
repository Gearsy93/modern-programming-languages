#include <iostream>
#include <Windows.h>
#include <fstream>

using namespace std;

struct print_count
{
    int solid;
    int broken;
    int overall;
};

// Состоания автомата (начальное, поиск начала пакета, обработка, проверка, конечное)
enum class state
{
    // Инициализация переменных
    start,

    // Ищем следующий байт синхронизации
    search_sync,

    // Проверяем наличие второго байта синхронизации
    handle_sync,

    // Проверяем, является ли длина пакета 45 или 2
    handle_len,

    // Считываем id + данные пакета + контрольную информацию
    handle_data,

    // Проверяем контрольную сумму
    handle_crc,

    // Выводим данные в текстовый файл
    write_to_file,

    // Завершение программы
    end
};

// Первичные и сигнальные
enum class data_type
{
    primary,
    signal
};

// Параметры данных
struct data_parameters
{
    int Ax;
    int Ay;
    int Az;
    int Wx;
    int Wy;
    int Wz;
    short Tax;
    short Tay;
    short Taz;
    short Twx;
    short Twy;
    short Twz;
    short S;
    short Timestamp;
    unsigned char Status;
    unsigned char Number;
};

void state_action(state& current_state, byte*& bytes_m, data_type& type, byte& current_byte, bool& check, print_count& counts);
void choose_next_state(state& current_state, byte& current_byte, bool& check, print_count& counts, byte*& bytes_m, data_type& type);
void initialization(state& current_state, byte*& bytes_m, data_type& type, bool& check, print_count& counts);
byte read_byte();
void read_data(byte data_length, byte*& byte_m);
bool check_crc(byte*& bytes_m, byte& current_byte, data_type type);
uint16_t crc16(const uint8_t* buf, int len);
void print_counts(print_count counts);
void print_data(data_type& type, byte*& bytes_m);
void finalizing(byte*& bytes_m);

ifstream input;
ofstream output_data, output_signal;

const uint16_t crc16tab[256] =
{
0x0000,0x1021,0x2042,0x3063,0x4084,0x50a5,0x60c6,0x70e7,
0x8108,0x9129,0xa14a,0xb16b,0xc18c,0xd1ad,0xe1ce,0xf1ef,
0x1231,0x0210,0x3273,0x2252,0x52b5,0x4294,0x72f7,0x62d6,
0x9339,0x8318,0xb37b,0xa35a,0xd3bd,0xc39c,0xf3ff,0xe3de,
0x2462,0x3443,0x0420,0x1401,0x64e6,0x74c7,0x44a4,0x5485,
0xa56a,0xb54b,0x8528,0x9509,0xe5ee,0xf5cf,0xc5ac,0xd58d,
0x3653,0x2672,0x1611,0x0630,0x76d7,0x66f6,0x5695,0x46b4,
0xb75b,0xa77a,0x9719,0x8738,0xf7df,0xe7fe,0xd79d,0xc7bc,
0x48c4,0x58e5,0x6886,0x78a7,0x0840,0x1861,0x2802,0x3823,
0xc9cc,0xd9ed,0xe98e,0xf9af,0x8948,0x9969,0xa90a,0xb92b,
0x5af5,0x4ad4,0x7ab7,0x6a96,0x1a71,0x0a50,0x3a33,0x2a12,
0xdbfd,0xcbdc,0xfbbf,0xeb9e,0x9b79,0x8b58,0xbb3b,0xab1a,
0x6ca6,0x7c87,0x4ce4,0x5cc5,0x2c22,0x3c03,0x0c60,0x1c41,
0xedae,0xfd8f,0xcdec,0xddcd,0xad2a,0xbd0b,0x8d68,0x9d49,
0x7e97,0x6eb6,0x5ed5,0x4ef4,0x3e13,0x2e32,0x1e51,0x0e70,
0xff9f,0xefbe,0xdfdd,0xcffc,0xbf1b,0xaf3a,0x9f59,0x8f78,
0x9188,0x81a9,0xb1ca,0xa1eb,0xd10c,0xc12d,0xf14e,0xe16f,
0x1080,0x00a1,0x30c2,0x20e3,0x5004,0x4025,0x7046,0x6067,
0x83b9,0x9398,0xa3fb,0xb3da,0xc33d,0xd31c,0xe37f,0xf35e,
0x02b1,0x1290,0x22f3,0x32d2,0x4235,0x5214,0x6277,0x7256,
0xb5ea,0xa5cb,0x95a8,0x8589,0xf56e,0xe54f,0xd52c,0xc50d,
0x34e2,0x24c3,0x14a0,0x0481,0x7466,0x6447,0x5424,0x4405,
0xa7db,0xb7fa,0x8799,0x97b8,0xe75f,0xf77e,0xc71d,0xd73c,
0x26d3,0x36f2,0x0691,0x16b0,0x6657,0x7676,0x4615,0x5634,
0xd94c,0xc96d,0xf90e,0xe92f,0x99c8,0x89e9,0xb98a,0xa9ab,
0x5844,0x4865,0x7806,0x6827,0x18c0,0x08e1,0x3882,0x28a3,
0xcb7d,0xdb5c,0xeb3f,0xfb1e,0x8bf9,0x9bd8,0xabbb,0xbb9a,
0x4a75,0x5a54,0x6a37,0x7a16,0x0af1,0x1ad0,0x2ab3,0x3a92,
0xfd2e,0xed0f,0xdd6c,0xcd4d,0xbdaa,0xad8b,0x9de8,0x8dc9,
0x7c26,0x6c07,0x5c64,0x4c45,0x3ca2,0x2c83,0x1ce0,0x0cc1,
0xef1f,0xff3e,0xcf5d,0xdf7c,0xaf9b,0xbfba,0x8fd9,0x9ff8,
0x6e17,0x7e36,0x4e55,0x5e74,0x2e93,0x3eb2,0x0ed1,0x1ef0
};

int main()
{
    SetConsoleOutputCP(1251);
    SetConsoleCP(1251);
    
    // Инициализация начальных переменных
    bool check;
    state current_state = state::start;
    byte* bytes_m;
    data_type type;
    byte current_byte;
    print_count counts;

    while (current_state != state::end)
    {
        state_action(current_state, bytes_m, type, current_byte, check, counts);
        choose_next_state(current_state, current_byte, check, counts, bytes_m, type);
    }
    print_counts(counts);
    finalizing(bytes_m);
}

// Действие при входе в состояние
void state_action(state &current_state, byte*& bytes_m, data_type &type, byte &current_byte, bool& check, print_count &counts)
{
    switch (current_state)
    {
        case state::start:
        {
            initialization(current_state, bytes_m, type, check, counts);
            break;
        }
        case state::search_sync:
        {
            current_byte = read_byte();
            break;
        }
        case state::handle_sync:
        {
            current_byte = read_byte();
            break;
        }
        case state::handle_len:
        {
            current_byte = read_byte();
            break;
        }
        case state::handle_data:
        {
            read_data(current_byte, bytes_m);
            break;
        }
        case state::handle_crc:
        {
            check = check_crc(bytes_m, current_byte, type);
            break;
        }
        case state::write_to_file:
        {
            print_data(type, bytes_m);
            break;
        }
    }
}

// Переход из текущего состояния в новое
void choose_next_state(state &current_state, byte &current_byte, bool &check, print_count& counts, byte* &bytes_m, data_type& type)
{
    switch (current_state)
    {
        case state::start: 
        {
            current_state = state::search_sync;
            break;
        }
        case state::search_sync:
        {
            if (input.eof())
            {
                current_state = state::end;
            }
            else
            {
                counts.overall += 1;
                if (current_byte == 0xAA)
                {
                    current_state = state::handle_sync;
                }
                else
                {
                    current_state = state::search_sync;
                }
            }
            break;
        }
        case state::handle_sync:
        {
            if (current_byte == 0xAA)
            {
                current_state = state::handle_len;
            }
            else
            {
                current_state = state::search_sync;
            }
            break;
        }
        case state::handle_len:
        {
            if (current_byte == 45 || current_byte == 2)
            {
                current_state = state::handle_data;
            }
            else
            {
                current_state = state::search_sync;
            }
            break;
        }
        case state::handle_data:
        {
            if (bytes_m[0] == 0x87)
            {
                current_state = state::handle_crc;
                type = data_type::primary;
            }
            else if (bytes_m[0] == 0x98)
            {
                current_state = state::handle_crc;
                type = data_type::signal;
            }
            else
            {
                current_state = state::search_sync;
            }
            break;
        }
        case state::handle_crc:
        {
            if (check)
            {
                check = false;
                counts.solid += 1;
                current_state = state::write_to_file;
            }
            else
            {
                counts.broken += 1;
                current_state = state::search_sync;
            }
            break;
        }
        case state::write_to_file:
        {
            current_state = state::search_sync;
        }
    }
}

// Инициализация начальных значений
void initialization(state &current_state, byte* &bytes_m, data_type &type, bool& check, print_count& counts)
{
    string file;
    check = false;
    counts.broken = 0;
    counts.overall = 0;
    counts.solid = 0;
    bytes_m = new byte[45];
    //мб надо добавить инициализацию типа но скорее нет

    while (true)
    {
        try
        {
            cout << "Введите путь до бинарного файла: ";
            cin >> file;
            input = ifstream(file, ios::binary);
            if (!input)
            {
                cout << "Ошибка во время открытия файла, повторите ввод" << endl;
                continue;
            }
            break;
        }
        catch (exception e)
        {
            cout << e.what() << endl;
        }
    }

    cout << "Введите имя выходного текстового файла: ";
    cin >> file;
    output_data = ofstream(file, ios::trunc);
    output_data << "Ax|Ay|Az|Wx|Wy|Wz|Tax|Tay|Taz|Twx|Twy|Tws|S|Timestamp|Status|Number" << endl;
    output_signal = ofstream("signals.txt", ios::trunc);
}

// Чтение одного байта
byte read_byte()
{
    byte current_byte;
    input.read(reinterpret_cast<char*>(&current_byte), sizeof(byte));
    return current_byte;
}

// Чтение данных пакета
void read_data(byte data_length, byte* &byte_m)
{
    input.read(reinterpret_cast<char*>(byte_m), sizeof(byte)* data_length);
}

// Проверка контрольной суммы
bool check_crc(byte*& bytes_m, byte& current_byte, data_type type)
{
    if (type == data_type::primary)
    {
        //Здесь может быть проблема, хз что выдает функция
        if (current_byte == 45 && *reinterpret_cast<short*>(&bytes_m[43]) == crc16(bytes_m, current_byte - 2)) return true;
        else return false;
    }
    else
    {
        if (current_byte == 2) return true;
        else return false;
    }
}

// CRC16
uint16_t crc16(const uint8_t* buf, int len)
{
    register int counter;
    register uint16_t crc = 0;
    for (counter = 0; counter < len; counter++)
        crc = (crc << 8) ^ crc16tab[((crc >> 8) ^ *(char*)buf++) & 0x00FF];
    return crc;
}

// Вывод информации о количестве первичных, сигнальных и битых пакетов
void print_counts(print_count counts)
{
    cout << "Целые пакеты: " << counts.solid << endl;
    cout << "Битые пакеты: " << counts.broken << endl;
    cout << "Всего пакетов: " << counts.overall << endl;
}

// Вывод данных в файл
void print_data(data_type& type, byte*& bytes_m)
{
    if (type == data_type::primary)
    {
        auto data  = *reinterpret_cast<data_parameters*>(bytes_m + 1);
        output_data << data.Ax << '|' << data.Ay << '|' << data.Az << '|' << data.Wx << '|' << data.Wy << '|' <<
            data.Wz << '|' << data.Tax << '|' << data.Tay << '|' << data.Taz << '|' << data.Twx << '|' << data.Twy << '|'
            << data.Twz << '|' << data.S << '|' << data.Timestamp - '\0' << '|' << data.Status - '\0' << '|' << data.Number - '\0' << endl;
    }
    else
    {
        output_signal << bytes_m[1] - '\0' << endl;
    }
}

// Освобождение памяти и закрытие файлов
void finalizing(byte*& bytes_m)
{
    delete[] bytes_m;
    input.close();
    output_data.close();
    output_signal.close();
}