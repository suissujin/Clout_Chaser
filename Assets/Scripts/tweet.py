import os
import sys

parent_dir = os.path.abspath(os.path.dirname(__file__))
vendor_dir = os.path.abspath(os.path.join(parent_dir, "..", "..", "PythonPackages"))
sys.path.append(vendor_dir)

from pytwitter import Api
from transformers import GPT2LMHeadModel, GPT2Tokenizer

from itertools import dropwhile

tokenizer = GPT2Tokenizer.from_pretrained(
    "gpt2",
)
model = GPT2LMHeadModel.from_pretrained("gpt2", pad_token_id=tokenizer.eos_token_id)

inputs = tokenizer.encode("Today I was", return_tensors="pt")
outputs = model.generate(
    inputs,
    max_length=60,
    temperature=4.0,
    do_sample=True,
    num_beams=5,
    early_stopping=True,
)


text = "".join(
    reversed(
        list(
            dropwhile(
                lambda x: x not in [".", "?", "!"],
                reversed(tokenizer.decode(outputs[0], skip_special_tokens=True)),
            )
        )
    )
)
print("Generated text:")
print("--------------")
print(text)
print("--------------")

api = Api(
    consumer_key=os.environ.get("CONSUMER_KEY"),
    consumer_secret=os.environ.get("CONSUMER_SECRET"),
    access_token=os.environ.get("ACCESS_TOKEN"),
    access_secret=os.environ.get("ACCESS_SECRET"),
)

api.create_tweet(text=text)
print("Posted tweet")
