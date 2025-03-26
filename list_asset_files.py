import os


def generate_tree(dir_path, prefix="", file_handle=None):
    """
    Recursively generate a tree-like structure of the directory and write it to a file.
    """
    # Get all items (files and directories) in the current directory
    items = sorted(os.listdir(dir_path))  # Sort for consistent output
    for index, item in enumerate(items):
        # Construct full path
        full_path = os.path.join(dir_path, item)
        # Determine if this is the last item in the current directory
        is_last = index == len(items) - 1
        # Choose the appropriate connector
        connector = "└── " if is_last else "├── "

        # Write the current item to the file
        line = f"{prefix}{connector}{item}"
        file_handle.write(line + "\n")

        # If it's a directory, recurse into it
        if os.path.isdir(full_path):
            # Adjust the prefix for the next level
            new_prefix = prefix + ("    " if is_last else "│   ")
            generate_tree(full_path, new_prefix, file_handle)


def save_assets_tree(base_path='.', output_file="assets_tree.txt"):
    """
    Save the contents of the './Assets' folder in a tree format to a text file.
    """
    # Define the specific Assets folder
    asset_path = os.path.join(base_path, 'Assets')

    # Check if the Assets folder exists
    if os.path.exists(asset_path) and os.path.isdir(asset_path):
        with open(output_file, 'w', encoding='utf-8') as f:
            f.write(f"Assets\n")
            generate_tree(asset_path, "", f)
        print(f"Tree structure saved to {output_file}")
    else:
        print(f"No 'Assets' folder found in {base_path}")


if __name__ == "__main__":
    print("Generating tree structure for 'Assets' folder and saving to file...\n")
    save_assets_tree()
