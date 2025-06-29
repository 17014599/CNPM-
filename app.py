from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/analyze', methods=['POST'])
def analyze():
    if 'file' not in request.files:
        return jsonify({'error': 'No file uploaded'}), 400

    file = request.files['file']
    filename = file.filename

    # Giả lập xử lý ảnh
    result = {
        "filename": filename,
        "diagnosis": "Tumor detected",
        "confidence": 0.93
    }

    return jsonify(result)

if __name__ == '__main__':
    app.run(port=5001)
