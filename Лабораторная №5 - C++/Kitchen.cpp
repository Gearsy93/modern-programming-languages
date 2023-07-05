#include <iostream>
#include <cstdio>
#include <string>
#include "Strings.h"

using namespace std;

// ������ � ���������
// �������� ������
class kitchenware {
public:
	virtual string name() { return {}; }
	int inventory_number{};
	explicit kitchenware() {}
	virtual ~kitchenware() = default;

	virtual void print(const bool show_id_status) {
		if (show_id_status)
			cout << "Inventory number: " << inventory_number << "\n";
	}
};

// �����
class stove : public virtual kitchenware {
public:
	string name() override { return "Stove"; }
	string color;
	explicit stove() {
		do {
			try {
				this->inventory_number = inventory_number;
				cout << enter_color;
				string color;
				cin >> color;
				this->color = color;
				break;
			}
			catch (exception) {
				cout << worst_enter;
			}
		} while (true);
	}

	void print(const bool show_id_status) override {
		kitchenware::print(show_id_status);
		cout << "Color: " << color << "\n";
	}
};

// ��������
class pan : public virtual kitchenware { 
public:
	string name() override { return "Pan"; }
	int capacity{};
	explicit pan() {
		do {
			try {
				this->inventory_number = inventory_number;
				cout << enter_capacity;
				int capacity;
				cin >> capacity;
				this->capacity = capacity;
				break;
			}
			catch (exception) {
				cout << worst_enter;
			}
		} while (true);
	}

	void print(const bool show_id_status) override {
		kitchenware::print(show_id_status);
		cout << "Capcacity: " << capacity << "\n";
	}
};

// ������������� �����
class electric_stove : public virtual stove {
public:
	string name() override { return "Electric stove"; }
	int power{};
	electric_stove() {
		do {
			try {
				this->inventory_number = inventory_number;
				this->color = color;
				cout << enter_power;
				int power;
				cin >> power;
				this->power = power;
				break;
			}
			catch (exception) {
				cout << worst_enter;
			}
		} while (true);
	}

	void print(bool const show_id_status) override {
		stove::print(show_id_status);
		cout << "Power: " << power << "\n";
	}
};

// ������� �����
class gas_stove : public virtual stove { 
public:
	string name() override { return "Gas stove"; }
	int gas_consumption{};
	gas_stove() {
		do {
			try {
				this->inventory_number = inventory_number;
				this->color = color;
				cout << enter_gas_consumption;
				int gas_consumption;
				cin >> gas_consumption;
				this->gas_consumption = gas_consumption;
				break;
			}
			catch (exception) {
				cout << worst_enter;
			}
		} while (true);
	}

	void print(bool const show_id_status) override {
		stove::print(show_id_status);
		cout << "Gas consumption: " << gas_consumption << "\n";
	}
};

// �����������
class multicooker : public electric_stove, public pan {
public:
	string name() override { return "Multicooker"; }
	bool is_a_pressure_cooker{};
	explicit multicooker() {
		do {
			try {
				this->inventory_number = inventory_number;
				this->power = power;
				this->color = color;
				cout << enter_is_the_pressure_cooker;
				string status;
				cin >> status; 
				this->is_a_pressure_cooker = status == "yes";
				break;
			}
			catch (exception) {
				cout << worst_enter;
			}
		} while (true);
	}

	void print(bool const show_id_status) override {
		electric_stove::print(show_id_status);
		pan::print(!show_id_status);
		if (is_a_pressure_cooker)
			cout << "Is a pressure cooker";
		else
			cout << "Isn't a pressure cooker";
	}
};

// ������ 
struct tree {
	kitchenware* data; // ����� �������� � ������
	tree* left; // ��������� �� ����� ���������
	tree* right; // ��������� �� ������ ���������
	int height;	// ������ ����

	explicit tree(kitchenware* id) { // �����������
		this->data = id;
		left = right = nullptr;
		height = 1;
	}
};

// �������
// ����� ���������� �� �������
void print_key(tree* p) {
	cout << p->data->name() << "\n";
	p->data->print(true);
	cout << spaces;
}

// �������� ������� ������� � ������
bool is_exist(tree* &p, const int id) {
	if (!p) { return false; }
	if (id == p->data->inventory_number) { return true; }
	if (id < p->data->inventory_number) { return is_exist(p->left, id); }
	if (id > p->data->inventory_number) { return is_exist(p->right, id); }
}

// ���������� ������ ������
int get_height(tree* p) { return p ? p->height : 0; } 

// ������� ����� �������� �����������
int balance_factor(tree* p) { return get_height(p->right) - get_height(p->left); } 

// ��������� ������ ����
void change_height(tree* p) { 
	auto height_left = get_height(p->left);
	auto height_right = get_height(p->right);
	p->height = (height_left > height_right ? height_left : height_right) + 1;
}

// ����� ������ �������
void rotate_rigth(tree* &p) {
	const auto q = p->left;
	p->left = q->right;
	q->right = p;
	change_height(p);
	p = q;
	change_height(p);
}

// ����� ����� �������
void rotate_left(tree* &p) {	
	const auto q = p->right;
	p->right = q->left;
	q->left = p;
	change_height(p);
	p = q;
	change_height(p);
}

// ������� ������ �������
void big_rotate_rigth(tree* &p) {
	rotate_left(p->left);
	rotate_rigth(p);
}

// ������� ����� �������
void big_rotate_left(tree* &p) {
	rotate_rigth(p->right);
	rotate_left(p);
}

// ������������ ����
void balancing(tree* &p) {
	change_height(p);
	const short bf = balance_factor(p);
	// ������ ����
	if (bf == 2) {
		if (balance_factor(p->right) >= 0) { rotate_left(p); }
		else { big_rotate_left(p); }
	}
	// ����� ����
	if (bf == -2) {
		if (balance_factor(p->left) <= 0) { rotate_rigth(p); }
		else { big_rotate_rigth(p); }
	}
}

// ���������� ������ ������� � ������
void include(tree* &p, kitchenware* k) { 
	if (!p) { p = new tree(k); }
	else
	{
		if (k->inventory_number < p->data->inventory_number) { include(p->left, k); }
		if (k->inventory_number > p->data->inventory_number) { include(p->right, k); }
		balancing(p);
	}
}

// ����� ���� � �������� �������
tree* find(tree* p, const int id) {
	if (!p) return nullptr;
	if (id < p->data->inventory_number) { return find(p->left, id); }
	if (id > p->data->inventory_number) { return find(p->right, id); }
	if (id == p->data->inventory_number) { return p; }
}

// ����� ���� � ����������� ������ � ������ 
tree* find_min(tree* p) { return p->left ? find_min(p->left) : p; }

// �������� ���� � ����������� ������ � ������
tree* remove_min(tree* p) {
	if (!(p->left)) { return p->right; }
	p->left = remove_min(p->left);
	balancing(p);
	return p;
}

// �������� �������� �� ������
tree* remove(tree* p, const int id) {
	if (!p) return nullptr;
	if (id < p->data->inventory_number) { p->left = remove(p->left, id); }
	if (id > p->data->inventory_number) { p->right = remove(p->right, id); }
	if (id == p->data->inventory_number) {
		const auto left = p->left;
		const auto rigth = p->right;
		delete p->data;
		delete p;
		if (!rigth) return left;
		auto min = find_min(rigth);
		min->right = remove_min(rigth);
		min->left = left;
		balancing(min);
		return min;
	}
	balancing(p);
	return p;
}

// �������� ������
void delete_tree(tree* p) { 
	if (!p) return;
	delete_tree(p->left);
	delete_tree(p->right);
	delete p->data;
	delete p;
}

// ������ ����� (������ - ����� - ������)
void pre_order(tree* p) { 
	if (!p) return;
	print_key(p);
	pre_order(p->left);
	pre_order(p->right);
}

// �������� ����� (����� - ������ - ������)
void post_order(tree* p) { 
	if (!p) return;
	post_order(p->left);
	post_order(p->right);
	print_key(p);
}

// ������������ ����� (����� - ������ - ������)
void in_order(tree* p) { 
	if (!p) return;
	in_order(p->left);
	print_key(p);
	in_order(p->right);
}

int main() {
	tree* tree = nullptr;
	auto exit_flag = true;
	int command, type_of_kitchenware, type_of_rotate;

	setlocale(LC_ALL, "Russian");
	while (exit_flag) {
		auto f = false;
		cout << enter_command;
		cin >> command;
		switch (command) {
		// ���������� ��������
		case 1: {
			cout << enter_kitchenware_type;
			cin >> type_of_kitchenware;
			kitchenware* obj = nullptr;
			// ����� ������ ������������ ��������
			switch (type_of_kitchenware) {
			case 1: {
				int id;
				cout << enter_id;
				cin >> id;
				if (!is_exist(tree, id)) {
					obj = new stove();
					obj->inventory_number = id;
					f = true;
				}
				else {
					cout << worst_add;
					const auto sub_tree = tree;
					print_key(find(sub_tree, id));
				}
				break;
			}
			case 2:  {
				int id;
				cout << enter_id;
				cin >> id;
				if (!is_exist(tree, id)) {
					obj = new pan();
					obj->inventory_number = id;
					f = true;
				}
				else {
					cout << worst_add;
					const auto sub_tree = tree;
					print_key(find(sub_tree, id));
				}
				break;
			}
			case 3: {
				int id;
				cout << enter_id;
				cin >> id;
				if (!is_exist(tree, id)) {
					obj = new electric_stove();
					obj->inventory_number = id;
					f = true;
				}
				else {
					cout << worst_add;
					const auto sub_tree = tree;
					print_key(find(sub_tree, id));
				}
				break;
			}
			case 4: {
				int id;
				cout << enter_id;
				cin >> id;
				if (!is_exist(tree, id)) {
					obj = new gas_stove();
					obj->inventory_number = id;
					f = true;
				}
				else {
					cout << worst_add;
					const auto sub_tree = tree;
					print_key(find(sub_tree, id));
				}
				break;
			}
			case 5: {
				int id;
				cout << enter_id;
				cin >> id;
				if (!is_exist(tree, id)) {
					obj = new multicooker();
					obj->inventory_number = id;
					f = true;
				}
				else {
					cout << worst_add;
					const auto sub_tree = tree;
					print_key(find(sub_tree, id));
				}
				break;
			}
			default: { cout << worst_enter; break; }
			}
			if (!obj)
				break;
			if (f) {
				include(tree, obj);
				cout << well_add;
			}
			break;
		}
		// �������� ��������
		case 2: {
			int number;
			cout << enter_deleted_item;
			cin >> number;
			if (is_exist(tree, number)) {
				tree = remove(tree, number);
				cout << well_delete;
			}
			else { cout << worst_delete; }
			break;
		}
		// ����� ����������
		case 3: {
			cout << enter_rotate;
			cin >> type_of_rotate;
			// ��� ������
			switch (type_of_rotate) {
			case 1: { cout << "\n"; pre_order(tree); break; }
			case 2: { cout << "\n"; post_order(tree); break; }
			case 3: { cout << "\n"; in_order(tree); break; }
			default: { cout << worst_enter; break; }
			}
			break;
		}
		// �����
		case 0: {
			delete_tree(tree);
			exit_flag = false;
			break;
		}
		default: { cout << worst_enter; break; }
		}
	}
	return 0;
}